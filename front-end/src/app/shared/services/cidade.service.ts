import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { IPagedResult } from '../models/paged-result.interface';
import { ICidade } from '../models/cidade.interface';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})

export class CidadeService {
  private readonly apiUrl = `${environment.apiUrl}/cidade`;

  constructor(private http: HttpClient) { }

  public obter(nome: string = '', page: number = 1, pageSize: number = 50, estadosIgnorados: string[] = []): Observable<IPagedResult<ICidade>> {
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());

    if (nome) {
      params = params.set('nome', nome);
    }

    if (estadosIgnorados && estadosIgnorados.length > 0) {
      estadosIgnorados.forEach(id => {
        params = params.append('estadosIgnorados', id);
      });
    }

    return this.http.get<IPagedResult<ICidade>>(
      this.apiUrl,
      { params }
    ).pipe(catchError((error) => {
      if (error.status === 404) {
        return of({ totalItems: 0, items: [] } as IPagedResult<ICidade>);
      }
      throw error;
    }));
  }
}
