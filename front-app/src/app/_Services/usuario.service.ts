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

    constructor(private http: HttpClient) {}

    getLogin(usuario: Usuario):Observable<Usuario>{
      return  this.http.post<Usuario>(this.baseUrl + '/login', usuario);
    }
}