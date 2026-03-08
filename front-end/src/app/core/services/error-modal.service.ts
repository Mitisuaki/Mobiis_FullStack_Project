import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { IErrorModalData } from '../models/error-modal-data.interface';

@Injectable({
  providedIn: 'root'
})

export class ErrorModalService {
  private modalStateSubject: Subject<IErrorModalData | null> = new Subject<IErrorModalData | null>();
  public modalState$: Observable<IErrorModalData | null> = this.modalStateSubject.asObservable();

  public mostrarMensagem(titulo: string, mensagens: string[]): void {
    this.modalStateSubject.next({ titulo, mensagens });
  }

  public fecharModal(): void {
    this.modalStateSubject.next(null);
  }
}
