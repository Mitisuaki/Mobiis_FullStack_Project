import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { IEstado } from '../models/estado.interface';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EstadoService {
  private readonly apiUrl = `${environment.apiUrl}/estado`;

  constructor(private http: HttpClient) { }

  public obter(nomeUf: string = ''): Observable<IEstado[]> {
    let params = new HttpParams();

    if (nomeUf) {
      params = params.set('nomeUf', nomeUf);
    }

    return this.http.get<IEstado[]>(
      this.apiUrl,
      { params }
    ).pipe(
      catchError((error) => {
        if (error.status === 404) {
          return of([]);
        }
        throw error;
      })
    );
  }
}
