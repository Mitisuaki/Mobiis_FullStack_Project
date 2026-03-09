// Adicione o HostListener na importação do @angular/core
import { Component, OnInit, OnDestroy, ElementRef, HostListener } from '@angular/core';
import { Subscription } from 'rxjs';
import { ErrorModalService } from '../../services/error-modal.service';
import { IErrorModalData } from '../../models/error-modal-data.interface';

@Component({
  selector: 'app-error-modal',
  templateUrl: './error-modal.component.html',
  styleUrls: ['./error-modal.component.scss']
})
export class ErrorModalComponent implements OnInit, OnDestroy {
  public dadosModal: IErrorModalData | null = null;
  private subscription: Subscription = new Subscription();

  constructor(
    private errorModalService: ErrorModalService,
    private el: ElementRef
  ) {}

  public ngOnInit(): void {
    document.body.appendChild(this.el.nativeElement);
    this.subscription = this.errorModalService.modalState$.subscribe(
      (dados: IErrorModalData | null) => {
        this.dadosModal = dados;
      }
    );

    document.addEventListener('keydown', this.onEscCapture, true);
  }

  private onEscCapture = (event: KeyboardEvent): void => {
    if (event.key === 'Escape' && this.dadosModal) {
      event.stopPropagation(); // Impede que o evento chegue aos outros modais
      event.stopImmediatePropagation();
      this.fechar();
    }
  };

  public fechar(): void {
    this.errorModalService.fecharModal();
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
    if (this.el.nativeElement && this.el.nativeElement.parentNode) {
      this.el.nativeElement.parentNode.removeChild(this.el.nativeElement);
    }
  }
}
