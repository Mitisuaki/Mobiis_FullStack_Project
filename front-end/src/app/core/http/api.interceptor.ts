import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ApiMessages } from '../constants/api-messages.constants';
import { ErrorModalService } from '../services/error-modal.service';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {

  constructor(private errorModalService: ErrorModalService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const apiReq: HttpRequest<unknown> = request.clone({});

    return next.handle(apiReq).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error.status === 404) {
          return throwError(error);
        }

        let mensagemFormatada: string[] = [ApiMessages.Erros.ErroDesconhecido];

        if (error.status === 400 && error.error)
        {
          const mensagens: string | string[] = error.error.mensagens;

          if (Array.isArray(mensagens))
          {
            mensagemFormatada = mensagens.map(String);

          } else if (typeof mensagens === 'string')
          {
            mensagemFormatada = [mensagens];

          } else
          {
            mensagemFormatada = [ApiMessages.Erros.ValidacaoGenerica];
          }

        } else if (error.status === 500)
        {
          mensagemFormatada = [ApiMessages.Erros.ErroServidor];
        }

        this.errorModalService.mostrarMensagem('Notificações', mensagemFormatada);

        return throwError(error);
      })
    );
  }
}
