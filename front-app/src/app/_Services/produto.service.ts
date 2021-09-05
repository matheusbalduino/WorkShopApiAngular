import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Produto } from '../_Interfaces/Produto';

@Injectable({
  providedIn: 'root'
})
export class ProdutoService {

  baseUrl = environment.baseUrl + "/Produto"

  constructor(private http: HttpClient) { }

  
  public getAllProducts(): Observable<Produto[]> {
    return this.http.get<Produto[]>(this.baseUrl);
  }

  public getByProdutoId(id : number):Observable<Produto>{
    return this.http.get<Produto>(`${this.baseUrl}/${id}`);
  }

  public postNewProduto(modal: Produto):Observable<Produto>{
    return this.http.post<Produto>(`${this.baseUrl}`, modal);
  }
  public delete(id: number): Observable<Produto>{
    return this.http.delete<Produto>(`${this.baseUrl}/${id}`)
  }

}