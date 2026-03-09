import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { IRegiao } from '../models/regiao.interface';
import { TipoExportacaoEnum } from '../../../shared/enums/tipo-exportacao.enum';
import { IRegiaoDetalhe } from '../models/regiao-detalhe.interface';

@Injectable({
  providedIn: 'root'
})
export class RegiaoService {
  private readonly apiUrl = `${environment.apiUrl}/regiao`;

  constructor(private http: HttpClient) { }

  public obterTodas(): Observable<IRegiao[]> {
    return this.http.get<IRegiao[]>(
      this.apiUrl
    );
  }

  public salvar(payload: any): Observable<any> {
    return this.http.post(
      this.apiUrl,
      payload
    );
  }

  public atualizar(id: string, payload: any): Observable<any> {
    return this.http.put(
      `${this.apiUrl}/${id}`,
      payload
    );
  }

  public obterPorId(id: string): Observable<IRegiaoDetalhe> {
    return this.http.get<IRegiaoDetalhe>(
      `${this.apiUrl}/${id}`
    );
  }

  public ativar(id: string): Observable<any> {
    return this.http.put(
      `${this.apiUrl}/${id}/ativar`,
      {}
    );
  }

  public inativar(id: string): Observable<any> {
    return this.http.put(
      `${this.apiUrl}/${id}/inativar`,
      {}
    );
  }

  public excluir(id: string): Observable<any> {
    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }

  public exportar(formato: TipoExportacaoEnum, ativo?: boolean): Observable<Blob> {
    let params: HttpParams = new HttpParams().set('formato', formato.toString());

    if (ativo !== undefined && ativo !== null) {
      params = params.set('ativo', ativo.toString());
    }

    return this.http.get(`${this.apiUrl}/exportar`, {
      params,
      responseType: 'blob'
    });
  }
}
