import { FileExtensions } from './../../../../core/constants/File-extensions.constants';
import { Component, HostListener, OnInit, OnDestroy } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { UiMessages } from '../../../../core/constants/ui-messages.constants';
import { TipoExportacaoEnum } from '../../../../shared/enums/tipo-exportacao.enum';
import { IRegiao } from '../../models/regiao.interface';
import { RegiaoService } from '../../services/regiao.service';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-regiao-list',
  templateUrl: './regiao-list.component.html',
  styleUrls: ['./regiao-list.component.scss']
})
export class RegiaoListComponent implements OnInit, OnDestroy {
  public regioes: IRegiao[] = [];
  public carregando: boolean = false;
  public textos = UiMessages.RegiaoList;
  public TipoExportacaoEnum = TipoExportacaoEnum;

  public exportDropdownAberto: boolean = false;
  public acoesDropdownId: string | null = null;
  public filtroMenuAberto: boolean = false;
  public filtroStatusMenuAberto = false;

  public termoFiltro: string = '';
  public ordenacaoDesc: boolean = false;
  public statusSelecionado: boolean | null = null;

  public paginaAtual: number = 1;
  public itensPorPagina: number = 15;

  private routerSub: Subscription = new Subscription();

  constructor(
    private regiaoService: RegiaoService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  public ngOnInit(): void {
    this.carregarRegioes();

    this.routerSub = this.router
      .events
      .pipe(
        filter(event => event instanceof NavigationEnd && event.urlAfterRedirects === '/regioes')
      ).subscribe(() => {
        this.carregarRegioes();
      });
  }

  public ngOnDestroy(): void {
    this.routerSub.unsubscribe();
  }

  @HostListener('document:click', ['$event'])
  public aoClicarFora(event: Event): void {
    const target = event.target as HTMLElement;
    if (target && typeof target.closest === 'function' && !target.closest('.menu-flutuante-container')) {
      this.fecharTodosMenus();
    }
  }

  public get regioesFiltradas(): IRegiao[] {
    let filtrado = this.regioes.filter((regiao: IRegiao) =>
      regiao.nome.toLowerCase().includes(this.termoFiltro.toLowerCase())
    );

    if (this.statusSelecionado !== null) {
      filtrado = filtrado.filter(r => r.ativo === this.statusSelecionado);
    }

    filtrado.sort((a: IRegiao, b: IRegiao) => {
      const res = a.nome.localeCompare(b.nome);
      return this.ordenacaoDesc ? -res : res;
    });

    return filtrado;
  }

  public get regioesPaginadas(): IRegiao[] {
    const inicio = (this.paginaAtual - 1) * this.itensPorPagina;
    return this.regioesFiltradas.slice(inicio, inicio + this.itensPorPagina);
  }

  public get totalPaginas(): number {
    return Math.ceil(this.regioesFiltradas.length / this.itensPorPagina);
  }

  public get paginasVisiveis(): number[] {
    const maxPaginasVisiveis = 5; // Limite máximo de botões na tela
    let inicio = Math.max(1, this.paginaAtual - Math.floor(maxPaginasVisiveis / 2));
    let fim = inicio + maxPaginasVisiveis - 1;

    if (fim > this.totalPaginas) {
      fim = this.totalPaginas;
      inicio = Math.max(1, fim - maxPaginasVisiveis + 1);
    }

    const paginas = [];
    for (let i = inicio; i <= fim; i++) {
      paginas.push(i);
    }
    return paginas;
  }

  public mudarPagina(pagina: number): void {
    if (pagina >= 1 && pagina <= this.totalPaginas) {
      this.paginaAtual = pagina;
      this.fecharTodosMenus();
    }
  }

  public onSearchInput(event: Event): void {
    this.termoFiltro = (event.target as HTMLInputElement).value;
    this.paginaAtual = 1;
  }

  public alterarOrdenacao(desc: boolean): void {
    this.ordenacaoDesc = desc;
    this.filtroMenuAberto = false;
  }

  public limparFiltro(): void {
    this.termoFiltro = '';
    this.ordenacaoDesc = false;
    this.filtroMenuAberto = false;
    this.paginaAtual = 1;
  }

  public filtrarPorStatus(status: boolean | null): void {
    this.statusSelecionado = status;
    this.filtroStatusMenuAberto = false;
    this.paginaAtual = 1;
  }

  private carregarRegioes(): void {
    this.carregando = true;
    this.regiaoService.obterTodas().subscribe((dados) => {
      this.regioes = dados;
      this.carregando = false;
    }, () => this.carregando = false);
  }

  public novaRegiao(): void {
    this.router.navigate(['/regioes/novo']);
  }

  public editarRegiao(id: string): void {
    this.router.navigate(['/regioes/editar', id]);
  }

  public excluirRegiao(id: string): void {
    Swal.fire({
      title: 'Confirmar exclusão',
      text: this.textos.ConfirmacaoExclusao,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#2b3e50',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Sim, excluir',
      cancelButtonText: 'Cancelar',
      reverseButtons: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.regiaoService.excluir(id).subscribe((r) => {
          this.toastr.success(r.mensagens);
          this.carregarRegioes();
        });
      }
    });
  }

  public alterarStatus(regiao: IRegiao): void {
    const request$ = regiao.ativo ? this.regiaoService.inativar(regiao.id) : this.regiaoService.ativar(regiao.id);
    request$.subscribe(() => { regiao.ativo = !regiao.ativo; });
  }

  private fecharTodosMenus(menuAtual?: string): void {
    if (menuAtual !== 'export') this.exportDropdownAberto = false;
    if (menuAtual !== 'acoes') this.acoesDropdownId = null;
    if (menuAtual !== 'filtroNome') this.filtroMenuAberto = false;
    if (menuAtual !== 'filtroStatus') this.filtroStatusMenuAberto = false;
  }

  public toggleExport(event: Event): void {
    event.stopPropagation();
    this.fecharTodosMenus('export');
    this.exportDropdownAberto = !this.exportDropdownAberto;
  }

  public toggleAcoes(id: string, event: Event): void {
    event.stopPropagation();
    this.fecharTodosMenus('acoes');
    this.acoesDropdownId = this.acoesDropdownId === id ? null : id;
  }

  public toggleFiltro(event: Event): void {
    event.stopPropagation();
    this.fecharTodosMenus('filtroNome');
    this.filtroMenuAberto = !this.filtroMenuAberto;
  }

  public toggleFiltroStatus(event: Event): void {
    event.stopPropagation();
    this.fecharTodosMenus('filtroStatus');
    this.filtroStatusMenuAberto = !this.filtroStatusMenuAberto;
  }

  public exportar(formato: TipoExportacaoEnum): void {
    this.exportDropdownAberto = false;
    this.carregando = true;

    this.regiaoService.exportar(formato).subscribe((blob: Blob) => {
      const url = window.URL.createObjectURL(blob);
      const link = document.createElement('a');
      link.href = url;
      const extensao = formato === TipoExportacaoEnum.Excel ? FileExtensions.Excel : FileExtensions.Csv;
      link.download = `Exportacao_Regioes.${extensao}`;
      link.click();
      window.URL.revokeObjectURL(url);
      this.carregando = false;
    }, () => this.carregando = false);
  }
}
