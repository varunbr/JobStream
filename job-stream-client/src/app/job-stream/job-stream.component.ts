import { Component, OnInit } from '@angular/core';
import { JobStreamService } from '../services/job-stream.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-job-stream',
  templateUrl: './job-stream.component.html',
  styleUrls: ['./job-stream.component.css'],
})
export class JobStreamComponent implements OnInit {
  jobStream: any;

  constructor(
    private jobStreamService: JobStreamService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.getJobStream(params['id']);
    });
  }

  getJobStream(id: number) {
    this.jobStreamService.getJobStream(id).subscribe((response) => {
      this.jobStream = response;
      console.log(response);
    });
  }

  addToQueue(id: number) {
    this.jobStreamService.addToQueue(id).subscribe((response) => {
      this.router.navigate(['/history/' + response.id]);
    });
  }
}
