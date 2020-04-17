import { Component, EventEmitter, OnInit, Output } from '@angular/core';

const SMALL_WIDTH_BREAKPOINT = 720;

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
})
export class LayoutComponent implements OnInit {
  title: string;
  @Output() redirect = new EventEmitter();
  menus: string[] = [];
  isDarkTheme: boolean;

  private mediaMatcher: MediaQueryList = matchMedia(
    `(max-width: ${SMALL_WIDTH_BREAKPOINT}px)`
  );

  userDisplayName = '';
  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
  toggleTheme() {
    this.isDarkTheme = !this.isDarkTheme;
  }

  ngOnInit() {}
  getTitle() {
    return (this.title = document.title);
  }
  isScreenSmall(): boolean {
    return this.mediaMatcher.matches;
  }
}
