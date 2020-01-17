import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-show-portfolio',
  templateUrl: './show-portfolio.component.html'
})
export class ShowPortfolioComponent {
  public portfolio: PositionVM[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<PositionVM[]>(baseUrl + 'portfolio').subscribe(result => {
      this.portfolio = result;
    }, error => console.error(error));
  }
}

interface MandateVM {
  name: string;
  allocation: number;
  value: number; 
}

interface PositionVM {
  code: string;
  name: string;
  value: number;
  mandates: MandateVM[];
}
