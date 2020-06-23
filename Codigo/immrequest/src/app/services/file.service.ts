import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment.prod';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  constructor(private http: HttpClient) {}
  validExtensions = ['json', 'xml'];

  upload(file: File): Promise<Observable<number[]>> {
    const extension: string = this.getExtension(file);

    if (!this.validExtensions.includes(extension)) {
      throw new Error(`la extensión "${extension}" no es soportada`);
    }

    return file.text().then((content: string) => {
      return this.http.post<number[]>(`${environment.serverUrl}/api/imports`, {
        content: content,
        fileType: extension,
      });
    });
  }

  private getExtension(file: File) {
    const indice = file.name.lastIndexOf('.');
    const extension: string = file.name.substr(indice + 1, file.name.length);
    return extension;
  }
}
