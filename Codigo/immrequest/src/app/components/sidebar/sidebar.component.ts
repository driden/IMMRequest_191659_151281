import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
  constructor() {}
  adminLinks: { text: string; route: any[]; icon: string }[] = [
    { text: 'Nuevo Tipo', route: ['/new-type'], icon: 'fas fa-kiwi-bird' },
    { text: 'Nueva solicitud', route: ['/new-request'], icon: 'fas fa-kiwi-bird' },
    { text: 'Solicitudes por ciudadano', route: ['/searchByMail'], icon: 'fas fa-kiwi-bird' },
    { text: 'Tipos más usados', route: ['/mostusedtypes'], icon: 'fas fa-kiwi-bird' },
    { text: 'Todas las Solicitudes', route: ['/view-all-requests'], icon: 'fas fa-kiwi-bird' },
    { text: 'Agregar un administrador', route: ['/add-admin'], icon: 'fas fa-kiwi-bird' },
    { text: 'Listar tipos', route: ['/view-all-types'], icon: 'fas fa-kiwi-bird' },
  ];

  citizenLinks:{ text: string; route: any[]; icon: string }[] = [
    { text: 'Crear nueva Solicitud', route: ['/new-request'], icon: 'fas fa-kiwi-bird' },
    { text: 'Importar desde archivo', route: ['/import'], icon: 'fas fa-kiwi-bird' },
  ];

  ngOnInit(): void {}
}
