import { Component, OnInit} from '@angular/core';
import { Produto } from '../_Interfaces/Produto';
import { ProdutoService } from '../_Services/produto.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ThisReceiver } from '@angular/compiler';

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
    this._Produto.imagemUrl = nomeArquivo[2];

    this.produto.postUpload(this.file, nomeArquivo[2])
      .subscribe(
        (resp) => {
          console.log(resp);
        },
        error => console.log(error)
      );
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

  validation() {
    this.registerForm = this.fb.group({
      nome:       ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      descricao:  ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      imagemUrl:  ['', Validators.required],
      preco:      ['' , Validators.required]
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
