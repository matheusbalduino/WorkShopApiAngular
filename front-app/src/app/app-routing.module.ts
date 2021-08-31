import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProdutosComponent } from './Produtos/Produtos.component';
import { UsuariosComponent } from './Usuarios/Usuarios.component';

const routes: Routes = [
  {path: "", component: ProdutosComponent},
  {path:"usuarios", component: UsuariosComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
