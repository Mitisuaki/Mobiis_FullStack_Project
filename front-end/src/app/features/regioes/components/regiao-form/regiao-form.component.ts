import { AfterViewInit, Component, HostListener, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TipoLocalidadeEnum } from '../../../../shared/enums/tipo-localidade.enum';
import { LocalidadeValidators } from '../../../../shared/validators/localidade.validator';
import { UiMessages } from '../../../../core/constants/ui-messages.constants';
import { RegiaoService } from '../../services/regiao.service';
import { ICidade } from 'src/app/shared/models/cidade.interface';
import { IEstado } from 'src/app/shared/models/estado.interface';
import { IRegiaoDetalhe } from '../../models/regiao-detalhe.interface';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-regiao-form',
  templateUrl: './regiao-form.component.html',
  styleUrls: ['./regiao-form.component.scss']
})
export class RegiaoFormComponent implements OnInit, AfterViewInit {
  public form: FormGroup;
  public isEdicao: boolean = false;
  public regiaoId: string | null = null;
  public TipoLocalidadeEnum = TipoLocalidadeEnum;
  public textos = UiMessages.RegiaoForm;

  public paginaAtual: number = 1;
  public itensPorPagina: number = 5;

  public get tituloPagina(): string {
    return this.isEdicao ? this.textos.TituloEdicao : this.textos.TituloNovo;
  }

  public get localidades(): FormArray {
    return this.form.get('localidades') as FormArray;
  }

  public get localidadesPaginadas(): any[] {
    const inicio = (this.paginaAtual - 1) * this.itensPorPagina;
    return this.localidades.controls.slice(inicio, inicio + this.itensPorPagina);
  }

  public get totalPaginas(): number {
    return Math.ceil(this.localidades.length / this.itensPorPagina);
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

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private regiaoService: RegiaoService,
    private toastr: ToastrService
  ) { }

  public ngOnInit(): void {
    this.montarForm();
    this.regiaoId = this.route.snapshot.paramMap.get('id');
    this.isEdicao = !!this.regiaoId;

    if (this.isEdicao) {
      this.carregarRegiao();
    } else {
      this.adicionarLocalidade(TipoLocalidadeEnum.Cidade);
    }
  }

  public ngAfterViewInit(): void {
  }

  private montarForm() {
    this.form = this.formBuilder.group({
      nome: ['', [Validators.required, Validators.maxLength(100)]],
      localidades: this.formBuilder.array([], [
        Validators.required,
        LocalidadeValidators.semDuplicidades(),
        LocalidadeValidators.semConflitoCidadeEstado()
      ])
    });
  }

  @HostListener('document:keydown.escape', ['$event'])
  public onEsc(event: KeyboardEvent): void {
    this.cancelar();
  }

  public getIndexAbsoluto(indexPaginado: number): number {
    return (this.paginaAtual - 1) * this.itensPorPagina + indexPaginado;
  }

  public mudarPagina(pagina: number): void {
    if (pagina >= 1 && pagina <= this.totalPaginas) {
      this.paginaAtual = pagina;
    }
  }

  public textoLabelLocalidade(indexAbsoluto: number): string {
    const tipo = this.localidades.at(indexAbsoluto).get('tipo')?.value;
    return tipo === TipoLocalidadeEnum.Estado ? this.textos.LabelEstado : this.textos.LabelCidade;
  }

  public isNomeInvalido(): boolean {
    const control = this.form.get('nome');
    return !!(control && control.invalid && control.touched);
  }

  public isLocalidadeInvalida(indexAbsoluto: number): boolean {
    return this.obterErrosLocalidade(indexAbsoluto).length > 0;
  }

  public adicionarLocalidade(tipo: TipoLocalidadeEnum): void {
    this.localidades.push(this.formBuilder.group({
      item: [null, Validators.required],
      tipo: [tipo]
    }));
    this.paginaAtual = this.totalPaginas;
  }

  public removerLocalidade(indexAbsoluto: number): void {
    this.localidades.removeAt(indexAbsoluto);
    if (this.paginaAtual > this.totalPaginas && this.paginaAtual > 1) {
      this.paginaAtual--;
    }
  }

  public obterErrosLocalidade(indexAbsoluto: number): string[] {
    const control = this.localidades.at(indexAbsoluto).get('item');
    const arrayErrors = this.localidades.errors;
    const erros: string[] = [];

    if (control?.invalid && control?.touched) {
      erros.push(this.textos.SelecaoObrigatoria);
    }

    const idAtual = control?.value?.id;
    if (idAtual) {
      if (arrayErrors?.['duplicidadeDetectada']?.includes(idAtual)) {
        erros.push(this.textos.LocalidadeDuplicada);
      }
      if (arrayErrors?.['conflitoDetectado']?.includes(idAtual)) {
        erros.push(this.textos.ErroCidadeNoEstado);
      }
    }
    return erros;
  }

  public obterEstadosIgnorados(): string[] {
    return this.localidades.value
      .filter((val: any) => val.item && val.tipo === TipoLocalidadeEnum.Estado)
      .map((val: any) => val.item.id);
  }

  private carregarRegiao(): void {
    this.regiaoService.obterPorId(this.regiaoId!).subscribe((regiao: IRegiaoDetalhe) => {
      this.form.patchValue({ nome: regiao.nome });
      this.localidades.clear();

      if (regiao.cidades && regiao.cidades.length > 0) {
        regiao.cidades.forEach((cidade: ICidade) => {
          this.localidades.push(this.formBuilder.group({
            item: [{
              id: cidade.id,
              nome: `${cidade.nome} - ${cidade.estado}`,
              tipo: TipoLocalidadeEnum.Cidade,
              estadoId: cidade.estadoId
            }, Validators.required],
            tipo: [TipoLocalidadeEnum.Cidade]
          }));
        });
      }

      if (regiao.estados && regiao.estados.length > 0) {
        regiao.estados.forEach((estado: IEstado) => {
          this.localidades.push(this.formBuilder.group({
            item: [{
              id: estado.id,
              nome: `${estado.sigla} - ${estado.nome}`,
              tipo: TipoLocalidadeEnum.Estado,
              estadoId: estado.id
            }, Validators.required],
            tipo: [TipoLocalidadeEnum.Estado]
          }));
        });
      }
    });
  }

  public salvar(): void {
    this.form.markAllAsTouched();

    const formValue = this.form.value;

    const regiao = {
      nome: formValue.nome,
      cidadesIds: formValue.localidades.filter((l: any) => l.tipo === TipoLocalidadeEnum.Cidade && l.item)
                                       .map((l: any) => l.item.id),
      estadosIds: formValue.localidades.filter((l: any) => l.tipo === TipoLocalidadeEnum.Estado && l.item)
                                       .map((l: any) => l.item.id)
    };

    const request$ = this.isEdicao
                     ? this.regiaoService.atualizar(this.regiaoId!, regiao)
                     : this.regiaoService.salvar(regiao);

    request$.subscribe((r) => {
      this.toastr.success(r.mensagens);
      this.router.navigate(['/regioes']);
    });
  }

  public cancelar(): void {
    this.router.navigate(['/regioes']);
  }
}
