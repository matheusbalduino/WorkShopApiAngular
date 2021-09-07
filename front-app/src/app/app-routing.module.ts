import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProdutosComponent } from './Produtos/Produtos.component';
import { UsuariosComponent } from './Usuarios/Usuarios.component';

const routes: Routes = [
  {path: "", component: ProdutosComponent},
  {path:"usuarios", component: UsuariosComponent},
  {path:"login", component: LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
