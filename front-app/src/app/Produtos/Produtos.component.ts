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
  
  /**------------Construtor com para quando iniciar, carregar as variáveis com as respectivas classes---------------- */
  constructor(private produto: ProdutoService,
    private modalService: NgbModal,
    private fb: FormBuilder,
    private router: Router) { 
     
    }
  
  // função Principal, roda todos os comandos necessários ao iniciar a página
  ngOnInit() {
    // verifica se o usuário está logado.
    if(this.login != 'true'){
      this.router.navigate(['/login']);
    }
    // carrega o formGroup com as validações
    this.validation();
    // carrega todos os eventos para serem exibidos.
    this.getEventos();

  }

  //função que conecta a rota de conexão com a api para buscar todos os produtos.
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
  // função que captura o evento, quando há alteração.
  // Usada para capturar a imagem carregada.
  onFileChange(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      this.file = event.target.files;
    }
  }

  // Função para atualizar
  uploadImage(){
    // Constante para o nome do arquivo, poderia ser uma variável
    // constante não tem seu valor alterado.
    const nomeArquivo = this._Produto.imagemUrl.split('\\', 3);

    //condicional para ver se foi inserido uma nova imagem quando faz update.
    // caso seja diferente de nulo, tem imagem e ele atualiza.
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

  //Função para criar e salvar o produto 
  saveProdutos(){
    //troca a virgula por ponto para não ter conflito com o tipo double.
    this.registerForm.value.preco = parseFloat(this.registerForm.value.preco.replace(",","."));
    
    //preenche objeto produto para envio à api
    this._Produto = Object.assign({},this.registerForm.value);
    this._Produto.usuarioId = parseInt(this.userId);
    //função para o upload de imagem ao servidor e para registro do nome do file.
    this.uploadImage();
    //acessa a rota de envio de postagem para criar novo objeto
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

  //função que atualiza o produto 
  updateProdutos(prod: Produto){
    console.log(this.registerFormUp.value.preco)

    // verifica se o valor é string para correções de formato
    if( typeof(this.registerFormUp.value.preco) === 'string')
      this.registerFormUp.value.preco = parseFloat(this.registerFormUp.value.preco.replace(",","."));
    
    // preenche o objeto produto para envio
    this._Produto = Object.assign({},this.registerFormUp.value);
    this._Produto.usuarioId = parseInt(this.userId);
    this._Produto.produtoId = prod.produtoId;
    //função para o upload de imagem ao servidor e para registro do nome do file.
    this.uploadImage();
    //Acesso a rota pelo service
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
  // Esta função deve ser iniciada no OnInit para criar a instâcia do FormGroup
  validation() {
    this.registerForm = this.fb.group({
      nome:       ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      descricao:  ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      imagemUrl:  ['', Validators.required],
      preco:      ['' , Validators.required]
    });
  }

  // Validation para atualizar Produto
  // Cria um objeto do tipo FormGRoup
  validationUp(item: Produto) {
    this.registerFormUp = this.fb.group({
      nome:       [item.nome, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      descricao:  [item.descricao, [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      imagemUrl:  [''],
      preco:      [item.preco , Validators.required]
    });
  }
  

  // variável do tipo objeto para alerta de modificações
  Alerts: any = {
    type: '',
    msg: ''
  }
  showAlert = false;
  dismissible: boolean = true;

  //Função para fechar o modal
  onClosed(): void {
    this.dismissible = !this.dismissible;
  }
  
  // Função para deletar um usuário pelo id
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
