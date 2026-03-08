import { IEstado } from '../../models/estado.interface';
import { Component, forwardRef, Input, OnInit, OnDestroy } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Subject, Observable, of, Subscription } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, finalize, map, switchMap, tap } from 'rxjs/operators';
import { IItemSelecionado } from '../../models/item-selecionado.interface';
import { TipoLocalidadeEnum } from '../../enums/tipo-localidade.enum';
import { CidadeService } from '../../services/cidade.service';
import { EstadoService } from '../../services/estado.service';
import { IPagedResult } from '../../models/paged-result.interface';
import { ICidade } from '../../models/cidade.interface';
import { UiMessages } from '../../../core/constants/ui-messages.constants';

@Component({
  selector: 'app-seletor-localidade',
  templateUrl: './seletor-localidade.component.html',
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => SeletorLocalidadeComponent), multi: true }]
})
export class SeletorLocalidadeComponent implements ControlValueAccessor, OnInit, OnDestroy {

  @Input() public tipoBusca: TipoLocalidadeEnum = TipoLocalidadeEnum.Cidade;
  @Input() public estadosIgnorados: string[] = [];

  public valorSelecionado: IItemSelecionado | null = null;
  public isDisabled: boolean = false;
  public textos = UiMessages.SeletorLocalidade;
  public bindLabel:string = "nome";

  public opcoes: IItemSelecionado[] = [];
  public carregando: boolean = false;
  public buscaSubject: Subject<string> = new Subject<string>();

  private termoBuscaAtual: string = '';
  private paginaAtual: number = 1;
  private totalCidades: number = 0;
  private inscricaoBusca = new Subscription();


  constructor(private cidadeService: CidadeService, private estadoService: EstadoService) { }

  private onChange: (value: IItemSelecionado | null) => void = () => {};
  private onTouched: () => void = () => {};

  public ngOnInit(): void {
    this.inscricaoBusca = this.buscaSubject.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      tap((termo: string) => {
        this.carregando = true;
        this.termoBuscaAtual = termo;
        this.paginaAtual = 1;
      }),
      switchMap((termo: string) =>
        this.buscarDados(termo, 1).pipe(
          catchError(() => of([])),
          finalize(() => this.carregando = false)
        )
      )
    ).subscribe((resultados: IItemSelecionado[]) => {
      this.opcoes = resultados;
    });

    this.carregarDadosIniciais();
  }

  public ngOnDestroy(): void {
    this.inscricaoBusca.unsubscribe();
  }

  private carregarDadosIniciais(): void {
    this.carregando = true;
    this.buscarDados('', 1).pipe(
      catchError(() => of([])),
      finalize(() => this.carregando = false)
    ).subscribe((resultados: IItemSelecionado[]) => {
      this.opcoes = resultados;
    });
  }

  private buscarDados(termo: string, pagina: number): Observable<IItemSelecionado[]> {
    if (this.tipoBusca === TipoLocalidadeEnum.Estado) {
      return this.estadoService
      .obter(termo)
      .pipe(
        map((estados: IEstado[]) => estados.map((e: IEstado) => ({
          id: e.id,
          nome: `${e.sigla} - ${e.nome}`,
          tipo: TipoLocalidadeEnum.Estado,
          estadoId: e.id
        }))),
        catchError(() => of([]))
      );
    } else {
      return this.cidadeService
      .obter(termo, pagina, 50, this.estadosIgnorados)
      .pipe(
        tap((res: IPagedResult<ICidade>) => this.totalCidades = res.totalItems),
        map((res: IPagedResult<ICidade>) => res.items.map((c: ICidade) => ({
          id: c.id,
          nome: `${c.nome} - ${c.estado}`,
          tipo: TipoLocalidadeEnum.Cidade,
          estadoId: c.estadoId
        }))),
        catchError(() => of([]))
      );
    }
  }

  public aoRolarFinal(): void {
    if (this.tipoBusca === TipoLocalidadeEnum.Cidade && this.opcoes.length < this.totalCidades && !this.carregando) {
      this.carregando = true;
      this.paginaAtual++;
      this.buscarDados(this.termoBuscaAtual, this.paginaAtual)
      .pipe(
        catchError(() => of([])),
        finalize(() => this.carregando = false))
      .subscribe(novos => {
        this.opcoes = [...this.opcoes, ...novos];
        this.carregando = false;
      });
    }
  }

  public writeValue(value: IItemSelecionado): void {
    this.valorSelecionado = value;
    if (value && !this.opcoes.find(o => o.id === value.id)) {
      this.opcoes = [value, ...this.opcoes];
    }
  }
  public registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  public setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  public atualizarValor(): void {
    this.onChange(this.valorSelecionado);
    this.onTouched();
  }

  public aoSair(): void {
    if (!this.valorSelecionado && this.opcoes.length > 0 && this.termoBuscaAtual) {
      this.valorSelecionado = this.opcoes[0];
      this.atualizarValor();
    }
    this.onTouched();
  }
}
