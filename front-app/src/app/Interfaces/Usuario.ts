import { Produto } from "./Produto";

export interface Usuario {
    usuarioId : number;
    nome : string;
    sobrenome : string;
    senha : string;
    produtos : Produto[];
}
