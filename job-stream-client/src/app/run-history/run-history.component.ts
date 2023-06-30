import { Component, OnInit } from '@angular/core';
import { JobStreamService } from '../services/job-stream.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-run-history',
  templateUrl: './run-history.component.html',
  styleUrls: ['./run-history.component.css'],
})
export class RunHistoryComponent implements OnInit {
  jobRun: any;

  constructor(
    private jobStreamService: JobStreamService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.getJobStream(params['id']);
    });
  }

  getJobStream(id: number) {
    this.jobStreamService.getJobStreamHistory(id).subscribe((response) => {
      this.jobRun = response;
      console.log(response);
    });
  }
}
