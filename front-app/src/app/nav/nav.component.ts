import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {
  public isCollapsed = false;
  constructor(private route: Router) { }

  ngOnInit() {

  }
  // função para limpar a session e fazer logout.
  logout(){
    sessionStorage.clear();
    this.route.navigate(['/login'])
  }

}
