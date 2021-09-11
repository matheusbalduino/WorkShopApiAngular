import { Component, OnInit} from '@angular/core';
import { Produto } from '../_Interfaces/Produto';
import { ProdutoService } from '../_Services/produto.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-Produtos',
  templateUrl: './Produtos.component.html',
  styleUrls: ['./Produtos.component.scss']
})
export class ProdutosComponent implements OnInit {

  /** Variáveis globais da classe */
  produtos: Produto[] = [];
  registerForm: FormGroup;
  registerFormUp: FormGroup;
  _Produto: Produto;
  
  file: File;
  login = sessionStorage.getItem('login');
  userId = sessionStorage.getItem('id') || '0';
  /**---------------------------- */
  constructor(private produto: ProdutoService,
    private modalService: NgbModal,
    private fb: FormBuilder,
    private router: Router) { 
     
    }

  ngOnInit() {
    if(this.login != 'true'){
      this.router.navigate(['/login']);
    }
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

  onFileChange(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
    }
  }

  uploadImage(){

    const nomeArquivo = this._Produto.imagemUrl.split('\\', 3);
    console.log("nome", nomeArquivo[2], nomeArquivo)
    //condicional para ver se é update ou novo produto
    if(nomeArquivo[2] != null){
      this._Produto.imagemUrl = nomeArquivo[2];
      this.produto.postUpload(this.file, nomeArquivo[2])
      .subscribe(
        (resp) => {
          console.log(resp);
        },
        error => console.log(error)
      );
    }
      
  }
  
  saveProdutos(){
    
    this.registerForm.value.preco = parseFloat(this.registerForm.value.preco.replace(",","."));
    
    this._Produto = Object.assign({},this.registerForm.value);
    this._Produto.usuarioId = parseInt(this.userId);
    this.uploadImage();

    this.produto.postNewProduto(this._Produto).subscribe(
      (resp) => {
        this.Alerts.msg = 'Produto Adicionado com Sucesso!'
        this.Alerts.type = 'success';
       this.showAlert = true;
       this.validation();
       this.getEventos();
      }
    );

  }

  updateProdutos(prod: Produto){
    console.log(this.registerFormUp.value.preco)

    if( typeof(this.registerFormUp.value.preco) === 'string')
      this.registerFormUp.value.preco = parseFloat(this.registerFormUp.value.preco.replace(",","."));
    
    this._Produto = Object.assign({},this.registerFormUp.value);
    this._Produto.usuarioId = parseInt(this.userId);
    this._Produto.produtoId = prod.produtoId;
    this.uploadImage();

    this.produto.updateProduto(this._Produto).subscribe(
      (resp) => {
        this.Alerts.msg = 'Produto Atualizado com Sucesso!'
        this.Alerts.type = 'success';
       this.showAlert = true;
       this.validation();
       this.getEventos();
      }
    );

  }

  // Validation para salvar o Produto
  // Cria a regra para criação do produto
  // é abastecido pela Tag Form que contem o registerForm
  // Esta função deve ser iniciada no OnInit para criar a instacia do FormGroup
  validation() {
    this.registerForm = this.fb.group({
      nome:       ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      descricao:  ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      imagemUrl:  ['', Validators.required],
      preco:      ['' , Validators.required]
    });
  }

  // Validation para atualizar Produto
  validationUp(item: Produto) {
    this.registerFormUp = this.fb.group({
      nome:       [item.nome, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      descricao:  [item.descricao, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      imagemUrl:  [''],
      preco:      [item.preco , Validators.required]
    });
  }
  


  Alerts: any = {
    type: '',
    msg: ''
  }
  showAlert = false;
  dismissible: boolean = true;

  onClosed(): void {
    this.dismissible = !this.dismissible;
  }

  deleteProduto(id: number){
    this.produto.delete(id).subscribe(
      (resp) => {
        
      },
      erro => {
        this.Alerts.msg = erro.error.text;
        this.Alerts.type = 'danger';
        this.showAlert = true;
        this.getEventos();
      }
    )
  }


}
