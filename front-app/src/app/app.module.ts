import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProdutosComponent } from './Produtos/Produtos.component';
import { UsuariosComponent } from './Usuarios/Usuarios.component';
import { NavComponent } from './nav/nav.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DemoCarouseBasicComponent } from './carousel/carousel.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';


@NgModule({
  declarations: [			
    AppComponent,
      ProdutosComponent,
      UsuariosComponent,
      NavComponent,
      DemoCarouseBasicComponent
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    NgbModule,
    CarouselModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
