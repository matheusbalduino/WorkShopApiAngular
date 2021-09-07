import { Produto } from "./Produto";

export interface Usuario {
    usuarioId : number;
    nome : string;
    sobrenome : string;
    senha : string;
    email: string;
    produtos : Produto[];
}
