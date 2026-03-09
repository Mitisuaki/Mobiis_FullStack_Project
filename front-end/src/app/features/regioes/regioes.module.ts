import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RegioesRoutingModule } from './regioes-routing.module';
import { RegioesComponent } from './regioes.component';
import { RegiaoListComponent } from './components/regiao-list/regiao-list.component';
import { RegiaoFormComponent } from './components/regiao-form/regiao-form.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    RegioesComponent,
    RegiaoListComponent,
    RegiaoFormComponent
  ],
  imports: [
    CommonModule,
    RegioesRoutingModule,
    SharedModule
  ]
})
export class RegioesModule { }
