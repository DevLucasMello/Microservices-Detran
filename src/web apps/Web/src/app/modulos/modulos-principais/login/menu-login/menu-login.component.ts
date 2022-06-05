import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu-login',
  templateUrl: './menu-login.component.html',
  styleUrls: ['./menu-login.component.css']
})
export class MenuLoginComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  } 

}
