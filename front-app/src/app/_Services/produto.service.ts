import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Produto } from '../_Interfaces/Produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  // url base para conexao com a api
  baseUrl = environment.baseUrl + "/Produto"

  //construtor do service, injeta a biblioteca de conexao com as rotas.
  constructor(private http: HttpClient) { }

  //conexao com a rota para recuperar todos os produtos
  public getAllProducts(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseUrl);
  }
  // Conexão com a rota para recuperar apenas 1 produto
  public getByProdutoId(id : number):Observable<Produto>{
    return this.http.get<Produto>(`${this.baseUrl}/${id}`);
  }
  // conexão com a rota para postar um produto
  public postNewProduto(modal: Produto):Observable<Produto>{
    return this.http.post<Produto>(`${this.baseUrl}`, modal);
  }
  // conexão com a rota para deletar um produto
  public delete(id: number): Observable<any>{
    return this.http.delete<any>(`${this.baseUrl}/Delete/${id}`)
  }
  // conexão com a rota para postar o upload da foto do produto
  public postUpload(file: any, name: string) {
    const fileToUpload = file[0];
    const formData = new FormData();
    formData.append('file', fileToUpload, name);

    return this.http.post(`${this.baseUrl}/upload`, formData);
  }
  // Conexão com a rota de atualizar produto
  public updateProduto(produto: Produto): Observable<Produto>{
    return this.http.put<Produto>(this.baseUrl + '/update', produto);
  }

}
