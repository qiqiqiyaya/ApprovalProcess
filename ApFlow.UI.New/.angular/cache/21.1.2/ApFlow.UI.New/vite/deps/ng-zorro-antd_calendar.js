import {
  NzOptionComponent,
  NzSelectComponent,
  NzSelectModule
} from "./chunk-H7SKDBW6.js";
import {
  NzRadioComponent,
  NzRadioGroupComponent,
  NzRadioModule
} from "./chunk-HM2PRZ7O.js";
import {
  DateTableComponent,
  LibPackerModule,
  MonthTableComponent
} from "./chunk-NXWNNHKV.js";
import "./chunk-DYJCJUBT.js";
import "./chunk-EXOTQ34R.js";
import {
  DateHelperService,
  NzI18nService
} from "./chunk-BR4Q7YWF.js";
import {
  CandyDate
} from "./chunk-5WIRSNGS.js";
import "./chunk-LQTPYJM5.js";
import "./chunk-S3LASCD3.js";
import "./chunk-WSY6IOJW.js";
import "./chunk-4LLLFSKS.js";
import "./chunk-3TU4IR26.js";
import "./chunk-XLC3SPSI.js";
import "./chunk-BT773X5Q.js";
import "./chunk-CJ5RG6CQ.js";
import "./chunk-PI4RSLRN.js";
import "./chunk-TQB722VF.js";
import "./chunk-5BO6G4N6.js";
import "./chunk-4OODPXLP.js";
import "./chunk-RJK3RDQK.js";
import "./chunk-B7XDWOSB.js";
import "./chunk-6DK53PHK.js";
import "./chunk-72DKPDI6.js";
import "./chunk-W6VE2EMK.js";
import "./chunk-U5YM4T6W.js";
import {
  NzStringTemplateOutletDirective
} from "./chunk-GRAHBMGU.js";
import "./chunk-QYDDKLT3.js";
import "./chunk-4U5FUHDN.js";
import {
  takeUntilDestroyed
} from "./chunk-PKOG7UK4.js";
import "./chunk-SJ4NT3YH.js";
import {
  Directionality
} from "./chunk-AMAGFN52.js";
import {
  FormsModule,
  NG_VALUE_ACCESSOR,
  NgControlStatus,
  NgModel
} from "./chunk-YAIECLDM.js";
import "./chunk-N2XNJ4XE.js";
import "./chunk-OXRDR26M.js";
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ContentChild,
  DestroyRef,
  Directive,
  EventEmitter,
  Input,
  NgModule,
  Output,
  TemplateRef,
  ViewEncapsulation,
  booleanAttribute,
  forwardRef,
  inject,
  setClassMetadata,
  ɵɵNgOnChangesFeature,
  ɵɵProvidersFeature,
  ɵɵadvance,
  ɵɵclassMap,
  ɵɵclassProp,
  ɵɵconditional,
  ɵɵconditionalCreate,
  ɵɵcontentQuery,
  ɵɵdefineComponent,
  ɵɵdefineDirective,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵelement,
  ɵɵelementContainerEnd,
  ɵɵelementContainerStart,
  ɵɵelementEnd,
  ɵɵelementStart,
  ɵɵgetCurrentView,
  ɵɵinterpolate1,
  ɵɵlistener,
  ɵɵloadQuery,
  ɵɵnextContext,
  ɵɵproperty,
  ɵɵqueryRefresh,
  ɵɵrepeater,
  ɵɵrepeaterCreate,
  ɵɵresetView,
  ɵɵrestoreView,
  ɵɵstyleProp,
  ɵɵtemplate,
  ɵɵtext,
  ɵɵtextInterpolate,
  ɵɵtwoWayBindingSet,
  ɵɵtwoWayListener,
  ɵɵtwoWayProperty
} from "./chunk-4XV3JIPT.js";
import "./chunk-XZWRYGZ6.js";
import "./chunk-6OM3666T.js";
import "./chunk-BQ76GOFF.js";
import "./chunk-DP5J3HDO.js";
import "./chunk-KWSTWQNB.js";

// node_modules/ng-zorro-antd/fesm2022/ng-zorro-antd-calendar.mjs
var _forTrack0 = ($index, $item) => $item.value;
function NzCalendarHeaderComponent_Conditional_0_ng_container_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtext(1);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵtextInterpolate(ctx_r0.nzCustomHeader);
  }
}
function NzCalendarHeaderComponent_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzCalendarHeaderComponent_Conditional_0_ng_container_0_Template, 2, 1, "ng-container", 1);
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    ɵɵproperty("nzStringTemplateOutlet", ctx_r0.nzCustomHeader);
  }
}
function NzCalendarHeaderComponent_Conditional_1_For_3_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-option", 3);
  }
  if (rf & 2) {
    const year_r3 = ctx.$implicit;
    ɵɵproperty("nzLabel", year_r3.label)("nzValue", year_r3.value);
  }
}
function NzCalendarHeaderComponent_Conditional_1_Conditional_4_For_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-option", 3);
  }
  if (rf & 2) {
    const month_r5 = ctx.$implicit;
    ɵɵproperty("nzLabel", month_r5.label)("nzValue", month_r5.value);
  }
}
function NzCalendarHeaderComponent_Conditional_1_Conditional_4_Template(rf, ctx) {
  if (rf & 1) {
    const _r4 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "nz-select", 8);
    ɵɵlistener("ngModelChange", function NzCalendarHeaderComponent_Conditional_1_Conditional_4_Template_nz_select_ngModelChange_0_listener($event) {
      ɵɵrestoreView(_r4);
      const ctx_r0 = ɵɵnextContext(2);
      return ɵɵresetView(ctx_r0.monthChange.emit($event));
    });
    ɵɵrepeaterCreate(1, NzCalendarHeaderComponent_Conditional_1_Conditional_4_For_2_Template, 1, 2, "nz-option", 3, _forTrack0);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext(2);
    ɵɵproperty("nzSize", ctx_r0.size)("nzDropdownMatchSelectWidth", false)("ngModel", ctx_r0.activeMonth);
    ɵɵadvance();
    ɵɵrepeater(ctx_r0.months);
  }
}
function NzCalendarHeaderComponent_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r2 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 0)(1, "nz-select", 2);
    ɵɵlistener("ngModelChange", function NzCalendarHeaderComponent_Conditional_1_Template_nz_select_ngModelChange_1_listener($event) {
      ɵɵrestoreView(_r2);
      const ctx_r0 = ɵɵnextContext();
      return ɵɵresetView(ctx_r0.updateYear($event));
    });
    ɵɵrepeaterCreate(2, NzCalendarHeaderComponent_Conditional_1_For_3_Template, 1, 2, "nz-option", 3, _forTrack0);
    ɵɵelementEnd();
    ɵɵconditionalCreate(4, NzCalendarHeaderComponent_Conditional_1_Conditional_4_Template, 3, 3, "nz-select", 4);
    ɵɵelementStart(5, "nz-radio-group", 5);
    ɵɵtwoWayListener("ngModelChange", function NzCalendarHeaderComponent_Conditional_1_Template_nz_radio_group_ngModelChange_5_listener($event) {
      ɵɵrestoreView(_r2);
      const ctx_r0 = ɵɵnextContext();
      ɵɵtwoWayBindingSet(ctx_r0.mode, $event) || (ctx_r0.mode = $event);
      return ɵɵresetView($event);
    });
    ɵɵlistener("ngModelChange", function NzCalendarHeaderComponent_Conditional_1_Template_nz_radio_group_ngModelChange_5_listener($event) {
      ɵɵrestoreView(_r2);
      const ctx_r0 = ɵɵnextContext();
      return ɵɵresetView(ctx_r0.modeChange.emit($event));
    });
    ɵɵelementStart(6, "label", 6);
    ɵɵtext(7);
    ɵɵelementEnd();
    ɵɵelementStart(8, "label", 7);
    ɵɵtext(9);
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵproperty("nzSize", ctx_r0.size)("nzDropdownMatchSelectWidth", false)("ngModel", ctx_r0.activeYear);
    ɵɵadvance();
    ɵɵrepeater(ctx_r0.years);
    ɵɵadvance(2);
    ɵɵconditional(ctx_r0.mode === "month" ? 4 : -1);
    ɵɵadvance();
    ɵɵtwoWayProperty("ngModel", ctx_r0.mode);
    ɵɵproperty("nzSize", ctx_r0.size);
    ɵɵadvance(2);
    ɵɵtextInterpolate(ctx_r0.monthTypeText);
    ɵɵadvance(2);
    ɵɵtextInterpolate(ctx_r0.yearTypeText);
  }
}
function NzCalendarComponent_Conditional_4_Template(rf, ctx) {
  if (rf & 1) {
    const _r1 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "date-table", 5);
    ɵɵlistener("valueChange", function NzCalendarComponent_Conditional_4_Template_date_table_valueChange_0_listener($event) {
      ɵɵrestoreView(_r1);
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.onDateSelect($event));
    });
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵproperty("prefixCls", ctx_r1.prefixCls)("value", ctx_r1.activeDate)("activeDate", ctx_r1.activeDate)("cellRender", ctx_r1.dateCell)("fullCellRender", ctx_r1.dateFullCell)("disabledDate", ctx_r1.nzDisabledDate);
  }
}
function NzCalendarComponent_Conditional_5_Template(rf, ctx) {
  if (rf & 1) {
    const _r3 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "month-table", 6);
    ɵɵlistener("valueChange", function NzCalendarComponent_Conditional_5_Template_month_table_valueChange_0_listener($event) {
      ɵɵrestoreView(_r3);
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.onDateSelect($event));
    });
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵproperty("prefixCls", ctx_r1.prefixCls)("value", ctx_r1.activeDate)("activeDate", ctx_r1.activeDate)("cellRender", ctx_r1.monthCell)("fullCellRender", ctx_r1.monthFullCell);
  }
}
var NzDateCellDirective = class _NzDateCellDirective {
  static ɵfac = function NzDateCellDirective_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzDateCellDirective)();
  };
  static ɵdir = ɵɵdefineDirective({
    type: _NzDateCellDirective,
    selectors: [["", "nzDateCell", ""]],
    exportAs: ["nzDateCell"]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzDateCellDirective, [{
    type: Directive,
    args: [{
      selector: "[nzDateCell]",
      exportAs: "nzDateCell"
    }]
  }], null, null);
})();
var NzMonthCellDirective = class _NzMonthCellDirective {
  static ɵfac = function NzMonthCellDirective_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzMonthCellDirective)();
  };
  static ɵdir = ɵɵdefineDirective({
    type: _NzMonthCellDirective,
    selectors: [["", "nzMonthCell", ""]],
    exportAs: ["nzMonthCell"]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzMonthCellDirective, [{
    type: Directive,
    args: [{
      selector: "[nzMonthCell]",
      exportAs: "nzMonthCell"
    }]
  }], null, null);
})();
var NzDateFullCellDirective = class _NzDateFullCellDirective {
  static ɵfac = function NzDateFullCellDirective_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzDateFullCellDirective)();
  };
  static ɵdir = ɵɵdefineDirective({
    type: _NzDateFullCellDirective,
    selectors: [["", "nzDateFullCell", ""]],
    exportAs: ["nzDateFullCell"]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzDateFullCellDirective, [{
    type: Directive,
    args: [{
      selector: "[nzDateFullCell]",
      exportAs: "nzDateFullCell"
    }]
  }], null, null);
})();
var NzMonthFullCellDirective = class _NzMonthFullCellDirective {
  static ɵfac = function NzMonthFullCellDirective_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzMonthFullCellDirective)();
  };
  static ɵdir = ɵɵdefineDirective({
    type: _NzMonthFullCellDirective,
    selectors: [["", "nzMonthFullCell", ""]],
    exportAs: ["nzMonthFullCell"]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzMonthFullCellDirective, [{
    type: Directive,
    args: [{
      selector: "[nzMonthFullCell]",
      exportAs: "nzMonthFullCell"
    }]
  }], null, null);
})();
var NzCalendarHeaderComponent = class _NzCalendarHeaderComponent {
  dateHelper = inject(DateHelperService);
  i18n = inject(NzI18nService);
  mode = "month";
  fullscreen = true;
  activeDate = new CandyDate();
  nzCustomHeader;
  modeChange = new EventEmitter();
  yearChange = new EventEmitter();
  monthChange = new EventEmitter();
  yearOffset = 10;
  yearTotal = 20;
  years = [];
  months = [];
  get activeYear() {
    return this.activeDate.getYear();
  }
  get activeMonth() {
    return this.activeDate.getMonth();
  }
  get size() {
    return this.fullscreen ? "default" : "small";
  }
  get yearTypeText() {
    return this.i18n.getLocale().Calendar.lang.year;
  }
  get monthTypeText() {
    return this.i18n.getLocale().Calendar.lang.month;
  }
  ngOnInit() {
    this.setUpYears();
    this.setUpMonths();
  }
  ngOnChanges(changes) {
    if (changes["activeDate"]) {
      const previousActiveDate = changes["activeDate"].previousValue;
      const currentActiveDate = changes["activeDate"].currentValue;
      if (previousActiveDate?.getYear() !== currentActiveDate?.getYear()) {
        this.setUpYears();
      }
    }
  }
  updateYear(year) {
    this.yearChange.emit(year);
    this.setUpYears(year);
  }
  setUpYears(year) {
    const start = (year || this.activeYear) - this.yearOffset;
    const end = start + this.yearTotal;
    this.years = [];
    for (let i = start; i < end; i++) {
      this.years.push({
        label: `${i}`,
        value: i
      });
    }
  }
  setUpMonths() {
    this.months = [];
    for (let i = 0; i < 12; i++) {
      const dateInMonth = this.activeDate.setMonth(i);
      const monthText = this.dateHelper.format(dateInMonth.nativeDate, "MMM");
      this.months.push({
        label: monthText,
        value: i
      });
    }
  }
  static ɵfac = function NzCalendarHeaderComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzCalendarHeaderComponent)();
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _NzCalendarHeaderComponent,
    selectors: [["nz-calendar-header"]],
    hostAttrs: [1, "ant-fullcalendar-header"],
    hostVars: 2,
    hostBindings: function NzCalendarHeaderComponent_HostBindings(rf, ctx) {
      if (rf & 2) {
        ɵɵstyleProp("display", "block");
      }
    },
    inputs: {
      mode: "mode",
      fullscreen: [2, "fullscreen", "fullscreen", booleanAttribute],
      activeDate: "activeDate",
      nzCustomHeader: "nzCustomHeader"
    },
    outputs: {
      modeChange: "modeChange",
      yearChange: "yearChange",
      monthChange: "monthChange"
    },
    exportAs: ["nzCalendarHeader"],
    features: [ɵɵNgOnChangesFeature],
    decls: 2,
    vars: 1,
    consts: [[1, "ant-picker-calendar-header"], [4, "nzStringTemplateOutlet"], [1, "ant-picker-calendar-year-select", 3, "ngModelChange", "nzSize", "nzDropdownMatchSelectWidth", "ngModel"], [3, "nzLabel", "nzValue"], [1, "ant-picker-calendar-month-select", 3, "nzSize", "nzDropdownMatchSelectWidth", "ngModel"], [1, "ant-picker-calendar-mode-switch", 3, "ngModelChange", "ngModel", "nzSize"], ["nz-radio-button", "", "nzValue", "month"], ["nz-radio-button", "", "nzValue", "year"], [1, "ant-picker-calendar-month-select", 3, "ngModelChange", "nzSize", "nzDropdownMatchSelectWidth", "ngModel"]],
    template: function NzCalendarHeaderComponent_Template(rf, ctx) {
      if (rf & 1) {
        ɵɵconditionalCreate(0, NzCalendarHeaderComponent_Conditional_0_Template, 1, 1, "ng-container")(1, NzCalendarHeaderComponent_Conditional_1_Template, 10, 8, "div", 0);
      }
      if (rf & 2) {
        ɵɵconditional(ctx.nzCustomHeader ? 0 : 1);
      }
    },
    dependencies: [NzSelectModule, NzOptionComponent, NzSelectComponent, FormsModule, NgControlStatus, NgModel, NzRadioModule, NzRadioComponent, NzRadioGroupComponent, NzStringTemplateOutletDirective],
    encapsulation: 2,
    changeDetection: 0
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzCalendarHeaderComponent, [{
    type: Component,
    args: [{
      encapsulation: ViewEncapsulation.None,
      changeDetection: ChangeDetectionStrategy.OnPush,
      selector: "nz-calendar-header",
      exportAs: "nzCalendarHeader",
      template: `
    @if (nzCustomHeader) {
      <ng-container *nzStringTemplateOutlet="nzCustomHeader">{{ nzCustomHeader }}</ng-container>
    } @else {
      <div class="ant-picker-calendar-header">
        <nz-select
          class="ant-picker-calendar-year-select"
          [nzSize]="size"
          [nzDropdownMatchSelectWidth]="false"
          [ngModel]="activeYear"
          (ngModelChange)="updateYear($event)"
        >
          @for (year of years; track year.value) {
            <nz-option [nzLabel]="year.label" [nzValue]="year.value" />
          }
        </nz-select>

        @if (mode === 'month') {
          <nz-select
            class="ant-picker-calendar-month-select"
            [nzSize]="size"
            [nzDropdownMatchSelectWidth]="false"
            [ngModel]="activeMonth"
            (ngModelChange)="monthChange.emit($event)"
          >
            @for (month of months; track month.value) {
              <nz-option [nzLabel]="month.label" [nzValue]="month.value" />
            }
          </nz-select>
        }

        <nz-radio-group
          class="ant-picker-calendar-mode-switch"
          [(ngModel)]="mode"
          (ngModelChange)="modeChange.emit($event)"
          [nzSize]="size"
        >
          <label nz-radio-button nzValue="month">{{ monthTypeText }}</label>
          <label nz-radio-button nzValue="year">{{ yearTypeText }}</label>
        </nz-radio-group>
      </div>
    }
  `,
      host: {
        class: "ant-fullcalendar-header",
        "[style.display]": `'block'`
      },
      imports: [NzSelectModule, FormsModule, NzRadioModule, NzStringTemplateOutletDirective]
    }]
  }], null, {
    mode: [{
      type: Input
    }],
    fullscreen: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    activeDate: [{
      type: Input
    }],
    nzCustomHeader: [{
      type: Input
    }],
    modeChange: [{
      type: Output
    }],
    yearChange: [{
      type: Output
    }],
    monthChange: [{
      type: Output
    }]
  });
})();
var NzCalendarComponent = class _NzCalendarComponent {
  cdr = inject(ChangeDetectorRef);
  directionality = inject(Directionality);
  destroyRef = inject(DestroyRef);
  activeDate = new CandyDate();
  prefixCls = "ant-picker-calendar";
  dir = "ltr";
  onChangeFn = () => {
  };
  onTouchFn = () => {
  };
  nzMode = "month";
  nzValue;
  nzDisabledDate;
  nzModeChange = new EventEmitter();
  nzPanelChange = new EventEmitter();
  nzSelectChange = new EventEmitter();
  nzValueChange = new EventEmitter();
  /**
   * Cannot use @Input and @ContentChild on one variable
   * because { static: false } will make @Input property get delayed
   **/
  nzDateCell;
  nzDateCellChild;
  get dateCell() {
    return this.nzDateCell || this.nzDateCellChild;
  }
  nzDateFullCell;
  nzDateFullCellChild;
  get dateFullCell() {
    return this.nzDateFullCell || this.nzDateFullCellChild;
  }
  nzMonthCell;
  nzMonthCellChild;
  get monthCell() {
    return this.nzMonthCell || this.nzMonthCellChild;
  }
  nzMonthFullCell;
  nzMonthFullCellChild;
  get monthFullCell() {
    return this.nzMonthFullCell || this.nzMonthFullCellChild;
  }
  nzCustomHeader;
  nzFullscreen = true;
  ngOnInit() {
    this.dir = this.directionality.value;
    this.directionality.change?.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(() => {
      this.dir = this.directionality.value;
    });
  }
  onModeChange(mode) {
    this.nzModeChange.emit(mode);
    this.nzPanelChange.emit({
      date: this.activeDate.nativeDate,
      mode
    });
  }
  onYearSelect(year) {
    const date = this.activeDate.setYear(year);
    this.updateDate(date);
  }
  onMonthSelect(month) {
    const date = this.activeDate.setMonth(month);
    this.updateDate(date);
  }
  onDateSelect(date) {
    this.updateDate(date);
  }
  writeValue(value) {
    this.updateDate(new CandyDate(value), false);
    this.cdr.markForCheck();
  }
  registerOnChange(fn) {
    this.onChangeFn = fn;
  }
  registerOnTouched(fn) {
    this.onTouchFn = fn;
  }
  updateDate(date, touched = true) {
    this.activeDate = date;
    if (touched) {
      this.onChangeFn(date.nativeDate);
      this.onTouchFn();
      this.nzSelectChange.emit(date.nativeDate);
      this.nzValueChange.emit(date.nativeDate);
    }
  }
  ngOnChanges(changes) {
    if (changes.nzValue) {
      this.updateDate(new CandyDate(this.nzValue), false);
    }
  }
  static ɵfac = function NzCalendarComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzCalendarComponent)();
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _NzCalendarComponent,
    selectors: [["nz-calendar"]],
    contentQueries: function NzCalendarComponent_ContentQueries(rf, ctx, dirIndex) {
      if (rf & 1) {
        ɵɵcontentQuery(dirIndex, NzDateCellDirective, 5, TemplateRef)(dirIndex, NzDateFullCellDirective, 5, TemplateRef)(dirIndex, NzMonthCellDirective, 5, TemplateRef)(dirIndex, NzMonthFullCellDirective, 5, TemplateRef);
      }
      if (rf & 2) {
        let _t;
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.nzDateCellChild = _t.first);
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.nzDateFullCellChild = _t.first);
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.nzMonthCellChild = _t.first);
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.nzMonthFullCellChild = _t.first);
      }
    },
    hostAttrs: [1, "ant-picker-calendar"],
    hostVars: 6,
    hostBindings: function NzCalendarComponent_HostBindings(rf, ctx) {
      if (rf & 2) {
        ɵɵclassProp("ant-picker-calendar-full", ctx.nzFullscreen)("ant-picker-calendar-mini", !ctx.nzFullscreen)("ant-picker-calendar-rtl", ctx.dir === "rtl");
      }
    },
    inputs: {
      nzMode: "nzMode",
      nzValue: "nzValue",
      nzDisabledDate: "nzDisabledDate",
      nzDateCell: "nzDateCell",
      nzDateFullCell: "nzDateFullCell",
      nzMonthCell: "nzMonthCell",
      nzMonthFullCell: "nzMonthFullCell",
      nzCustomHeader: "nzCustomHeader",
      nzFullscreen: [2, "nzFullscreen", "nzFullscreen", booleanAttribute]
    },
    outputs: {
      nzModeChange: "nzModeChange",
      nzPanelChange: "nzPanelChange",
      nzSelectChange: "nzSelectChange",
      nzValueChange: "nzValueChange"
    },
    exportAs: ["nzCalendar"],
    features: [ɵɵProvidersFeature([{
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => _NzCalendarComponent),
      multi: true
    }]), ɵɵNgOnChangesFeature],
    decls: 6,
    vars: 8,
    consts: [[3, "modeChange", "yearChange", "monthChange", "fullscreen", "activeDate", "nzCustomHeader", "mode"], [1, "ant-picker-panel"], [1, "ant-picker-body"], [3, "prefixCls", "value", "activeDate", "cellRender", "fullCellRender", "disabledDate"], [3, "prefixCls", "value", "activeDate", "cellRender", "fullCellRender"], [3, "valueChange", "prefixCls", "value", "activeDate", "cellRender", "fullCellRender", "disabledDate"], [3, "valueChange", "prefixCls", "value", "activeDate", "cellRender", "fullCellRender"]],
    template: function NzCalendarComponent_Template(rf, ctx) {
      if (rf & 1) {
        ɵɵelementStart(0, "nz-calendar-header", 0);
        ɵɵtwoWayListener("modeChange", function NzCalendarComponent_Template_nz_calendar_header_modeChange_0_listener($event) {
          ɵɵtwoWayBindingSet(ctx.nzMode, $event) || (ctx.nzMode = $event);
          return $event;
        });
        ɵɵlistener("modeChange", function NzCalendarComponent_Template_nz_calendar_header_modeChange_0_listener($event) {
          return ctx.onModeChange($event);
        })("yearChange", function NzCalendarComponent_Template_nz_calendar_header_yearChange_0_listener($event) {
          return ctx.onYearSelect($event);
        })("monthChange", function NzCalendarComponent_Template_nz_calendar_header_monthChange_0_listener($event) {
          return ctx.onMonthSelect($event);
        });
        ɵɵelementEnd();
        ɵɵelementStart(1, "div", 1)(2, "div")(3, "div", 2);
        ɵɵconditionalCreate(4, NzCalendarComponent_Conditional_4_Template, 1, 6, "date-table", 3)(5, NzCalendarComponent_Conditional_5_Template, 1, 5, "month-table", 4);
        ɵɵelementEnd()()();
      }
      if (rf & 2) {
        ɵɵproperty("fullscreen", ctx.nzFullscreen)("activeDate", ctx.activeDate)("nzCustomHeader", ctx.nzCustomHeader);
        ɵɵtwoWayProperty("mode", ctx.nzMode);
        ɵɵadvance(2);
        ɵɵclassMap(ɵɵinterpolate1("ant-picker-", ctx.nzMode === "month" ? "date" : "month", "-panel"));
        ɵɵadvance(2);
        ɵɵconditional(ctx.nzMode === "month" ? 4 : 5);
      }
    },
    dependencies: [NzCalendarHeaderComponent, LibPackerModule, DateTableComponent, MonthTableComponent],
    encapsulation: 2,
    changeDetection: 0
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzCalendarComponent, [{
    type: Component,
    args: [{
      encapsulation: ViewEncapsulation.None,
      changeDetection: ChangeDetectionStrategy.OnPush,
      selector: "nz-calendar",
      exportAs: "nzCalendar",
      template: `
    <nz-calendar-header
      [fullscreen]="nzFullscreen"
      [activeDate]="activeDate"
      [nzCustomHeader]="nzCustomHeader"
      [(mode)]="nzMode"
      (modeChange)="onModeChange($event)"
      (yearChange)="onYearSelect($event)"
      (monthChange)="onMonthSelect($event)"
    ></nz-calendar-header>

    <div class="ant-picker-panel">
      <div class="ant-picker-{{ nzMode === 'month' ? 'date' : 'month' }}-panel">
        <div class="ant-picker-body">
          @if (nzMode === 'month') {
            <!--  TODO(@wenqi73) [cellRender] [fullCellRender] -->
            <date-table
              [prefixCls]="prefixCls"
              [value]="activeDate"
              [activeDate]="activeDate"
              [cellRender]="$any(dateCell)"
              [fullCellRender]="$any(dateFullCell)"
              [disabledDate]="nzDisabledDate"
              (valueChange)="onDateSelect($event)"
            ></date-table>
          } @else {
            <month-table
              [prefixCls]="prefixCls"
              [value]="activeDate"
              [activeDate]="activeDate"
              [cellRender]="$any(monthCell)"
              [fullCellRender]="$any(monthFullCell)"
              (valueChange)="onDateSelect($event)"
            ></month-table>
          }
        </div>
      </div>
    </div>
  `,
      host: {
        class: "ant-picker-calendar",
        "[class.ant-picker-calendar-full]": "nzFullscreen",
        "[class.ant-picker-calendar-mini]": "!nzFullscreen",
        "[class.ant-picker-calendar-rtl]": `dir === 'rtl'`
      },
      providers: [{
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => NzCalendarComponent),
        multi: true
      }],
      imports: [NzCalendarHeaderComponent, LibPackerModule]
    }]
  }], null, {
    nzMode: [{
      type: Input
    }],
    nzValue: [{
      type: Input
    }],
    nzDisabledDate: [{
      type: Input
    }],
    nzModeChange: [{
      type: Output
    }],
    nzPanelChange: [{
      type: Output
    }],
    nzSelectChange: [{
      type: Output
    }],
    nzValueChange: [{
      type: Output
    }],
    nzDateCell: [{
      type: Input
    }],
    nzDateCellChild: [{
      type: ContentChild,
      args: [NzDateCellDirective, {
        static: false,
        read: TemplateRef
      }]
    }],
    nzDateFullCell: [{
      type: Input
    }],
    nzDateFullCellChild: [{
      type: ContentChild,
      args: [NzDateFullCellDirective, {
        static: false,
        read: TemplateRef
      }]
    }],
    nzMonthCell: [{
      type: Input
    }],
    nzMonthCellChild: [{
      type: ContentChild,
      args: [NzMonthCellDirective, {
        static: false,
        read: TemplateRef
      }]
    }],
    nzMonthFullCell: [{
      type: Input
    }],
    nzMonthFullCellChild: [{
      type: ContentChild,
      args: [NzMonthFullCellDirective, {
        static: false,
        read: TemplateRef
      }]
    }],
    nzCustomHeader: [{
      type: Input
    }],
    nzFullscreen: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }]
  });
})();
var NzCalendarModule = class _NzCalendarModule {
  static ɵfac = function NzCalendarModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzCalendarModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NzCalendarModule,
    imports: [NzCalendarHeaderComponent, NzCalendarComponent, NzDateCellDirective, NzDateFullCellDirective, NzMonthCellDirective, NzMonthFullCellDirective],
    exports: [NzCalendarComponent, NzDateCellDirective, NzDateFullCellDirective, NzMonthCellDirective, NzMonthFullCellDirective]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [NzCalendarHeaderComponent, NzCalendarComponent]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzCalendarModule, [{
    type: NgModule,
    args: [{
      imports: [NzCalendarHeaderComponent, NzCalendarComponent, NzDateCellDirective, NzDateFullCellDirective, NzMonthCellDirective, NzMonthFullCellDirective],
      exports: [NzCalendarComponent, NzDateCellDirective, NzDateFullCellDirective, NzMonthCellDirective, NzMonthFullCellDirective]
    }]
  }], null, null);
})();
export {
  NzCalendarComponent,
  NzCalendarHeaderComponent,
  NzCalendarModule,
  NzDateCellDirective,
  NzDateFullCellDirective,
  NzMonthCellDirective,
  NzMonthFullCellDirective
};
//# sourceMappingURL=ng-zorro-antd_calendar.js.map
