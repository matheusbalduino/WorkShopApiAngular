import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Usuario } from '../_Interfaces/Usuario';
import { UsuarioService } from '../_Services/usuario.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  falseLogin = false;
  constructor(private usuario: UsuarioService,
    private router: Router) { }
  
  public user: Usuario = {
    usuarioId: 0,
    nome: '',
    sobrenome: '',
    senha: '',
    email: '',
    produtos : [
      {
      produtoId: 0,
      nome: '',
      descricao: '',
      preco: 0,
      imagemUrl: '',
      usuarioId: 0
    }
  ] 
  };

  ngOnInit() {
   
  }

  login(){
   
    this.usuario.getLogin(this.user).subscribe(
      (resp: any) => {
        
        if(resp != null){
          sessionStorage.setItem('login', 'true');
          sessionStorage.setItem('id',resp.usuarioId);
          this.router.navigate([`/`]);
        }
      },
      error => {
        this.falseLogin = true;
      }
    )
      
    }

}
