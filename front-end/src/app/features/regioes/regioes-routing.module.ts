import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RegiaoFormComponent } from './components/regiao-form/regiao-form.component';
import { RegiaoListComponent } from './components/regiao-list/regiao-list.component';

const routes: Routes = [
  {
    path: '',
    component: RegiaoListComponent,
    children: [
      { path: 'novo', component: RegiaoFormComponent },
      { path: 'editar/:id', component: RegiaoFormComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RegioesRoutingModule { }
