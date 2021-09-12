import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { Usuario } from "../_Interfaces/Usuario";

@Injectable({
  providedIn: 'root'
})

export class UsuarioService{
    
  baseUrl = environment.baseUrl + "/Usuario"

  //construtor do service, injeta a biblioteca de conexao com as rotas.
    constructor(private http: HttpClient) {}
  // Conexao com a rota para verificar o login
    getLogin(usuario: Usuario):Observable<Usuario>{
      return  this.http.post<Usuario>(this.baseUrl + '/login', usuario);
    }
}