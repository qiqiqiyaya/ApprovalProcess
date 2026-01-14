import { Component, OnInit } from '@angular/core';
import { FlowChartComponent } from './components/flow-chart/flow-chart.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [FlowChartComponent]
})
export class AppComponent implements OnInit {
  title = 'X6 Angular 流程图示例';
  public isLoading = true;

  ngOnInit(): void {
    // 模拟加载过程
    setTimeout(() => {
      this.isLoading = false;
    }, 1500);
  }
}