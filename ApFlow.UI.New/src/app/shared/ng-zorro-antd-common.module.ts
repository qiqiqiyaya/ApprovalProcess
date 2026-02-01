import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzPopoverModule } from 'ng-zorro-antd/popover';
import { NzTreeModule } from 'ng-zorro-antd/tree';
import { NzTreeSelectModule } from 'ng-zorro-antd/tree-select';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzSliderModule } from 'ng-zorro-antd/slider';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzProgressModule } from 'ng-zorro-antd/progress';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzResultModule } from 'ng-zorro-antd/result';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzDescriptionsModule } from 'ng-zorro-antd/descriptions';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzStepsModule } from 'ng-zorro-antd/steps';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzTransferModule } from 'ng-zorro-antd/transfer';
import { NzCalendarModule } from 'ng-zorro-antd/calendar';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { NzRateModule } from 'ng-zorro-antd/rate';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { NzTimelineModule } from 'ng-zorro-antd/timeline';
import { NzAnchorModule } from 'ng-zorro-antd/anchor';
import { NzAffixModule } from 'ng-zorro-antd/affix';
import { NzIconModule } from 'ng-zorro-antd/icon';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    // 导出所有ng-zorro-antd模块，供其他模块使用
    NzButtonModule,
    NzCardModule,
    NzDrawerModule,
    NzIconModule,
    NzModalModule,
    NzGridModule,
    NzTabsModule,
    NzRadioModule,
    NzTableModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    NzCheckboxModule,
    NzDropDownModule,
    NzMenuModule,
    NzPopoverModule,
    NzTreeModule,
    NzTreeSelectModule,
    NzDatePickerModule,
    NzTimePickerModule,
    NzSwitchModule,
    NzSliderModule,
    NzUploadModule,
    NzProgressModule,
    NzSpinModule,
    NzAlertModule,
    NzTagModule,
    NzResultModule,
    NzListModule,
    NzDescriptionsModule,
    NzDividerModule,
    NzTagModule,
    NzBadgeModule,
    NzAvatarModule,
    NzSkeletonModule,
    NzEmptyModule,
    NzBreadCrumbModule,
    NzStepsModule,
    NzPaginationModule,
    NzCollapseModule,
    NzTransferModule,
    NzCalendarModule,
    NzCarouselModule,
    NzRateModule,
    NzStatisticModule,
    NzTimelineModule,
    NzAnchorModule,
    NzAffixModule
  ]
})
export class NgZorroAntdCommonModule { }
