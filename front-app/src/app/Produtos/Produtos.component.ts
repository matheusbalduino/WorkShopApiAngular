import { Component, OnInit} from '@angular/core';
import { Produto } from '../Interfaces/Produto';
import { ProdutoService } from '../Services/produto.service';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-Produtos',
  templateUrl: './Produtos.component.html',
  styleUrls: ['./Produtos.component.scss']
})
export class ProdutosComponent implements OnInit {

  /** Variáveis globais da classe */
  produtos: Produto[] = [];
  

  /**---------------------------- */
  constructor(private produto: ProdutoService,
    private modalService: NgbModal) { }

  ngOnInit() {
    this.getEventos();

  }

  getEventos(){
    this.produto.getAllProducts().subscribe(
      (resposta: Produto[]) => {
        this.produtos = resposta;
      },
      error => console.log(error),
    );
  }

  openEdit(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then();
  }

}
