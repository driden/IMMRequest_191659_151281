import { Directive,HostBinding,HostListener} from '@angular/core';

@Directive({
  selector: '[appDropdown]',
  exportAs:'appDropdown'
})
export class DropdownDirective {

  @HostBinding('class.open') isOpen = false;

  @HostListener('click') toggleopen() {
     console.log(1);
    this.isOpen = !this.isOpen;
  }
}
