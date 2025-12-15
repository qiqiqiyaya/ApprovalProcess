import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-approver-configuration',
  templateUrl: './approver-configuration.component.html',
  styleUrls: ['./approver-configuration.component.css'],
  standalone: false
})
export class ApproverConfigurationComponent implements OnInit {

  radioValue = 'A';
  constructor() { }

  ngOnInit() {
  }

}
