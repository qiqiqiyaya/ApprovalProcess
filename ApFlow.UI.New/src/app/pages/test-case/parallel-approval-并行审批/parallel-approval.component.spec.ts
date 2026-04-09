import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ParallelApprovalComponent } from './parallel-approval.component';
import { FlowGraphModule } from '../../flow-graph/flow-graph.module';
import { NgZorroAntdCommonModule } from '../../../shared/ng-zorro-antd-common.module';


describe('ParallelApprovalComponent', () => {
  let component: ParallelApprovalComponent;
  let fixture: ComponentFixture<ParallelApprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        FlowGraphModule,
        NgZorroAntdCommonModule
      ],
      declarations: [ ParallelApprovalComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ParallelApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize flow graph', () => {
    expect(component.flowGraph).toBeDefined();
    expect(component.flowGraph.nodes.length).toBeGreaterThan(0);
    expect(component.flowGraph.edges.length).toBeGreaterThan(0);
  });
});
