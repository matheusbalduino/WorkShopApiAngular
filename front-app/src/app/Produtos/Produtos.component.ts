import { Component, OnInit} from '@angular/core';
import { Produto } from '../_Interfaces/Produto';
import { ProdutoService } from '../_Services/produto.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-Produtos',
  templateUrl: './Produtos.component.html',
  styleUrls: ['./Produtos.component.scss']
})
export class ProdutosComponent implements OnInit {

  /** Variáveis globais da classe */
  produtos: Produto[] = [];
  registerForm: FormGroup;
  _Produto: Produto;
 
  /**---------------------------- */
  constructor(private produto: ProdutoService,
    private modalService: NgbModal,
    private fb: FormBuilder) { }

  ngOnInit() {
    this.validation();
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

  openModal(content: any) {
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then();
  }
  
  saveProdutos(){
    
    console.log(this.registerForm);
    this._Produto = this.registerForm.value;

    this.produto.postNewProduto(this._Produto).subscribe(
      (resp) => {
        console.log("enviou");
       this.showAlert = true;
      }
    );

  }

  validation() {
    this.registerForm = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      descricao: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      imagemUrl: ['', Validators.required],
      preco: ['', [Validators.required, Validators.max(120000)]],
    });
  }
  
  Alerts: any= 
    {
      type: 'success',
      msg: `Produto Adicionado com Sucesso!`
    }
  showAlert = false;
  dismissible: boolean = true;
  onClosed(): void {
    this.dismissible = !this.dismissible;
  }

}
