import { Component, OnInit } from '@angular/core';

import { AuthService } from 'src/app/services/auth.service';
import { User } from 'src/app/models/User';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  user: User = null;
  constructor(private authService: AuthService) {}

  adminLinks: { text: string; route: any[]; icon: string }[] = [
    {
      text: 'Solicitudes por ciudadano',
      route: ['/search-by-mail'],
      icon: 'fas fa-kiwi-bird',
    },
    {
      text: 'Tipos más usados',
      route: ['/most-used-types'],
      icon: 'fas fa-kiwi-bird',
    },
    {
      text: 'Todas las Solicitudes',
      route: ['/view-all-requests'],
      icon: 'fas fa-kiwi-bird',
    },
    {
      text: 'Agregar un administrador',
      route: ['/add-admin'],
      icon: 'fas fa-kiwi-bird',
    },
    { text: 'Nuevo Tipo', route: ['/new-type'], icon: 'fas fa-kiwi-bird' },
    {
      text: 'Listar tipos',
      route: ['/view-all-types'],
      icon: 'fas fa-kiwi-bird',
    },
  ];

  citizenLinks: { text: string; route: any[]; icon: string }[] = [
    {
      text: 'Nueva Solicitud',
      route: ['/new-request'],
      icon: 'fas fa-kiwi-bird',
    },
    {
      text: 'Mis solicitudes',
      route: ['/view-my-requests'],
      icon: 'fas fa-kiwi-bird',
    },
    {
      text: 'Importar desde archivo',
      route: ['/import'],
      icon: 'fas fa-kiwi-bird',
    },
  ];

  ngOnInit(): void {
    this.authService.userSubject.subscribe((user) => (this.user = user));
  }

  logout(): void {
    this.authService.logout();
  }
}
