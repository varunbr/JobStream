import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-job-block',
  templateUrl: './job-block.component.html',
  styleUrls: ['./job-block.component.css'],
})
export class JobBlockComponent {
  @Input() block: any;
}
