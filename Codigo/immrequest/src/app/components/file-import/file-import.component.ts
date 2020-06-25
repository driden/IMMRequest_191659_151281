import { Component, OnInit } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { tap } from 'rxjs/internal/operators/tap';

@Component({
  selector: 'app-file-import',
  templateUrl: './file-import.component.html',
  styleUrls: ['./file-import.component.css'],
})
export class FileImportComponent implements OnInit {
  errorMsg = '';
  success = false;
  addedIds: number[] = [];
  constructor(private fileService: FileService) {}

  ngOnInit(): void {}

  upload(files: FileList) {
    const file: File = files.item(0);
    this.fileService
      .upload(file)
      .then((obs) => {
        obs
          .pipe(
            tap(
              (ids: number[]) => {
                this.success = true;
                this.addedIds = ids;
              },
              () => (this.errorMsg = 'ocurrió un error')
            )
          )
          .subscribe();
      })
      .catch((e) => (this.errorMsg = e));
  }
}
