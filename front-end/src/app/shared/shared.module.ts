import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { SeletorLocalidadeComponent } from './components/seletor-localidade/seletor-localidade.component';

@NgModule({
  declarations: [
    SeletorLocalidadeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    SeletorLocalidadeComponent
  ]
})

export class SharedModule { }
