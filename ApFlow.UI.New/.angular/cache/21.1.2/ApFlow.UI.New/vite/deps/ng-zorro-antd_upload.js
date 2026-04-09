import {
  NzProgressComponent,
  NzProgressModule
} from "./chunk-2S2HGWRR.js";
import {
  NzTooltipDirective,
  NzTooltipModule
} from "./chunk-XJ7HWFMH.js";
import {
  NzI18nService
} from "./chunk-AWU3GB3T.js";
import "./chunk-6DTLTA2W.js";
import "./chunk-LMQQNRJH.js";
import "./chunk-GLLB55ZV.js";
import "./chunk-ZHVRDCSU.js";
import "./chunk-4OODPXLP.js";
import "./chunk-RJK3RDQK.js";
import {
  ENTER
} from "./chunk-B7XDWOSB.js";
import {
  withAnimationCheck
} from "./chunk-SPYBVWE7.js";
import "./chunk-QYDDKLT3.js";
import {
  NzButtonComponent,
  NzButtonModule
} from "./chunk-MVUM2WJ4.js";
import "./chunk-BLMJUYRV.js";
import "./chunk-XLC3SPSI.js";
import {
  NzTransitionPatchDirective
} from "./chunk-3TU4IR26.js";
import "./chunk-CWBX6YEP.js";
import {
  Directionality
} from "./chunk-AMAGFN52.js";
import {
  NzIconDirective,
  NzIconModule
} from "./chunk-HQM66OPU.js";
import "./chunk-72DKPDI6.js";
import {
  Platform
} from "./chunk-W6VE2EMK.js";
import "./chunk-M622CJQC.js";
import {
  takeUntilDestroyed
} from "./chunk-PKOG7UK4.js";
import {
  fromEventOutsideAngular,
  generateClassName,
  toBoolean,
  warn
} from "./chunk-HP6B2NEN.js";
import "./chunk-6OM3666T.js";
import "./chunk-BQ76GOFF.js";
import {
  HttpClient,
  HttpEventType,
  HttpHeaders,
  HttpRequest,
  HttpResponse
} from "./chunk-N2XNJ4XE.js";
import {
  NgTemplateOutlet
} from "./chunk-OXRDR26M.js";
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  DOCUMENT,
  DestroyRef,
  ElementRef,
  EventEmitter,
  Input,
  NgModule,
  NgZone,
  Output,
  ViewChild,
  ViewEncapsulation,
  booleanAttribute,
  computed,
  inject,
  input,
  numberAttribute,
  setClassMetadata,
  signal,
  ɵɵNgOnChangesFeature,
  ɵɵadvance,
  ɵɵanimateEnter,
  ɵɵanimateLeave,
  ɵɵattribute,
  ɵɵclassMap,
  ɵɵclassProp,
  ɵɵconditional,
  ɵɵconditionalCreate,
  ɵɵdeclareLet,
  ɵɵdefineComponent,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵdomElementEnd,
  ɵɵdomElementStart,
  ɵɵdomListener,
  ɵɵdomProperty,
  ɵɵelement,
  ɵɵelementContainer,
  ɵɵelementEnd,
  ɵɵelementStart,
  ɵɵgetCurrentView,
  ɵɵinterpolate1,
  ɵɵinterpolate2,
  ɵɵlistener,
  ɵɵloadQuery,
  ɵɵnextContext,
  ɵɵprojection,
  ɵɵprojectionDef,
  ɵɵproperty,
  ɵɵpureFunction0,
  ɵɵpureFunction1,
  ɵɵqueryRefresh,
  ɵɵreadContextLet,
  ɵɵreference,
  ɵɵrepeater,
  ɵɵrepeaterCreate,
  ɵɵrepeaterTrackByIdentity,
  ɵɵresetView,
  ɵɵrestoreView,
  ɵɵsanitizeUrl,
  ɵɵstoreLet,
  ɵɵstyleMap,
  ɵɵstyleProp,
  ɵɵtemplate,
  ɵɵtemplateRefExtractor,
  ɵɵtext,
  ɵɵtextInterpolate1,
  ɵɵviewQuery
} from "./chunk-4XV3JIPT.js";
import {
  Observable,
  Subscription,
  filter,
  map,
  of,
  switchMap,
  tap
} from "./chunk-XZWRYGZ6.js";
import "./chunk-DP5J3HDO.js";
import {
  __spreadValues
} from "./chunk-KWSTWQNB.js";

// node_modules/ng-zorro-antd/fesm2022/ng-zorro-antd-upload.mjs
var _c0 = ["file"];
var _c1 = ["nz-upload-btn", ""];
var _c2 = ["*"];
var _c3 = (a0) => ({
  $implicit: a0
});
var _c4 = () => ({
  opacity: 0.5,
  "pointer-events": "none"
});
function NzUploadListComponent_For_2_ng_template_2_Case_0_ng_template_1_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_2_Case_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 15);
    ɵɵtemplate(1, NzUploadListComponent_For_2_ng_template_2_Case_0_ng_template_1_Template, 0, 0, "ng-template", 16);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(2).$implicit;
    const iconNode_r4 = ɵɵreference(5);
    ɵɵclassProp("ant-upload-list-item-file", !file_r3.isUploading);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", iconNode_r4)("ngTemplateOutletContext", ɵɵpureFunction1(4, _c3, file_r3));
  }
}
function NzUploadListComponent_For_2_ng_template_2_Case_1_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "img", 18);
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(3).$implicit;
    ɵɵproperty("src", file_r3.thumbUrl || file_r3.url, ɵɵsanitizeUrl);
    ɵɵattribute("alt", file_r3.name);
  }
}
function NzUploadListComponent_For_2_ng_template_2_Case_1_Conditional_2_ng_template_0_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_2_Case_1_Conditional_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzUploadListComponent_For_2_ng_template_2_Case_1_Conditional_2_ng_template_0_Template, 0, 0, "ng-template", 16);
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(3).$implicit;
    const iconNode_r4 = ɵɵreference(5);
    ɵɵproperty("ngTemplateOutlet", iconNode_r4)("ngTemplateOutletContext", ɵɵpureFunction1(2, _c3, file_r3));
  }
}
function NzUploadListComponent_For_2_ng_template_2_Case_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r5 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "a", 17);
    ɵɵlistener("click", function NzUploadListComponent_For_2_ng_template_2_Case_1_Template_a_click_0_listener($event) {
      ɵɵrestoreView(_r5);
      const file_r3 = ɵɵnextContext(2).$implicit;
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.handlePreview(file_r3, $event));
    });
    ɵɵconditionalCreate(1, NzUploadListComponent_For_2_ng_template_2_Case_1_Conditional_1_Template, 1, 2, "img", 18)(2, NzUploadListComponent_For_2_ng_template_2_Case_1_Conditional_2_Template, 1, 4, null, 16);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(2).$implicit;
    ɵɵclassProp("ant-upload-list-item-file", !file_r3.isImageUrl);
    ɵɵproperty("href", file_r3.url || file_r3.thumbUrl, ɵɵsanitizeUrl);
    ɵɵadvance();
    ɵɵconditional(file_r3.isImageUrl ? 1 : 2);
  }
}
function NzUploadListComponent_For_2_ng_template_2_Case_2_ng_template_1_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_2_Case_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 14);
    ɵɵtemplate(1, NzUploadListComponent_For_2_ng_template_2_Case_2_ng_template_1_Template, 0, 0, "ng-template", 16);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(2).$implicit;
    const iconNode_r4 = ɵɵreference(5);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", iconNode_r4)("ngTemplateOutletContext", ɵɵpureFunction1(2, _c3, file_r3));
  }
}
function NzUploadListComponent_For_2_ng_template_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_2_Case_0_Template, 2, 6, "div", 12)(1, NzUploadListComponent_For_2_ng_template_2_Case_1_Template, 3, 4, "a", 13)(2, NzUploadListComponent_For_2_ng_template_2_Case_2_Template, 2, 4, "div", 14);
  }
  if (rf & 2) {
    let tmp_18_0;
    const file_r3 = ɵɵnextContext().$implicit;
    ɵɵconditional((tmp_18_0 = file_r3.iconType) === "uploading" ? 0 : tmp_18_0 === "thumbnail" ? 1 : 2);
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_0_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-icon", 20);
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_0_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-icon", 21);
  }
  if (rf & 2) {
    const file_r6 = ɵɵnextContext(3).$implicit;
    ɵɵproperty("nzType", file_r6.isImageUrl ? "picture" : "file");
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_0_Conditional_0_Template, 1, 0, "nz-icon", 20)(1, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_0_Conditional_1_Template, 1, 1, "nz-icon", 21);
  }
  if (rf & 2) {
    const file_r6 = ɵɵnextContext(2).$implicit;
    ɵɵconditional(file_r6.isUploading ? 0 : 1);
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_1_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtext(0);
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext(5);
    ɵɵtextInterpolate1(" ", ctx_r1.locale.uploading, " ");
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_1_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-icon", 21);
  }
  if (rf & 2) {
    const file_r6 = ɵɵnextContext(3).$implicit;
    ɵɵproperty("nzType", file_r6.isImageUrl ? "picture" : "file");
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_1_Conditional_0_Template, 1, 1)(1, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_1_Conditional_1_Template, 1, 1, "nz-icon", 21);
  }
  if (rf & 2) {
    const file_r6 = ɵɵnextContext(2).$implicit;
    ɵɵconditional(file_r6.isUploading ? 0 : 1);
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-icon", 19);
  }
  if (rf & 2) {
    const file_r6 = ɵɵnextContext(2).$implicit;
    ɵɵproperty("nzType", file_r6.isUploading ? "loading" : "paper-clip");
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_0_Template, 2, 1)(1, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_1_Template, 2, 1)(2, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Case_2_Template, 1, 1, "nz-icon", 19);
  }
  if (rf & 2) {
    let tmp_20_0;
    ɵɵnextContext(3);
    const _listType_r7 = ɵɵreadContextLet(0);
    ɵɵconditional((tmp_20_0 = _listType_r7) === "picture" ? 0 : tmp_20_0 === "picture-card" ? 1 : 2);
  }
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_1_ng_template_0_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_4_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzUploadListComponent_For_2_ng_template_4_Conditional_1_ng_template_0_Template, 0, 0, "ng-template", 16);
  }
  if (rf & 2) {
    const file_r6 = ɵɵnextContext().$implicit;
    const ctx_r1 = ɵɵnextContext(2);
    ɵɵproperty("ngTemplateOutlet", ctx_r1.iconRender)("ngTemplateOutletContext", ɵɵpureFunction1(2, _c3, file_r6));
  }
}
function NzUploadListComponent_For_2_ng_template_4_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_4_Conditional_0_Template, 3, 1)(1, NzUploadListComponent_For_2_ng_template_4_Conditional_1_Template, 1, 4, null, 16);
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext(2);
    ɵɵconditional(!ctx_r1.iconRender ? 0 : 1);
  }
}
function NzUploadListComponent_For_2_ng_template_6_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    const _r8 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "button", 23);
    ɵɵlistener("click", function NzUploadListComponent_For_2_ng_template_6_Conditional_0_Template_button_click_0_listener($event) {
      ɵɵrestoreView(_r8);
      const file_r3 = ɵɵnextContext(2).$implicit;
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.handleRemove(file_r3, $event));
    });
    ɵɵelement(1, "nz-icon", 24);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext(3);
    ɵɵattribute("title", ctx_r1.locale.removeFile);
  }
}
function NzUploadListComponent_For_2_ng_template_6_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_6_Conditional_0_Template, 2, 1, "button", 22);
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext(2);
    ɵɵconditional(ctx_r1.icons.showRemoveIcon ? 0 : -1);
  }
}
function NzUploadListComponent_For_2_ng_template_8_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    const _r9 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "button", 23);
    ɵɵlistener("click", function NzUploadListComponent_For_2_ng_template_8_Conditional_0_Template_button_click_0_listener() {
      ɵɵrestoreView(_r9);
      const file_r3 = ɵɵnextContext(2).$implicit;
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.handleDownload(file_r3));
    });
    ɵɵelement(1, "nz-icon", 25);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext(3);
    ɵɵattribute("title", ctx_r1.locale.downloadFile);
  }
}
function NzUploadListComponent_For_2_ng_template_8_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_8_Conditional_0_Template, 2, 1, "button", 22);
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext().$implicit;
    ɵɵconditional(file_r3.showDownload ? 0 : -1);
  }
}
function NzUploadListComponent_For_2_ng_template_10_Conditional_0_ng_template_1_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_10_Conditional_0_ng_template_2_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_10_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "span", 27);
    ɵɵtemplate(1, NzUploadListComponent_For_2_ng_template_10_Conditional_0_ng_template_1_Template, 0, 0, "ng-template", 10)(2, NzUploadListComponent_For_2_ng_template_10_Conditional_0_ng_template_2_Template, 0, 0, "ng-template", 10);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    ɵɵnextContext(2);
    const removeIcon_r10 = ɵɵreference(7);
    const downloadIcon_r11 = ɵɵreference(9);
    ɵɵnextContext();
    const _listType_r7 = ɵɵreadContextLet(0);
    ɵɵclassProp("picture", _listType_r7 === "picture");
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", downloadIcon_r11);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", removeIcon_r10);
  }
}
function NzUploadListComponent_For_2_ng_template_10_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_10_Conditional_0_Template, 3, 4, "span", 26);
  }
  if (rf & 2) {
    ɵɵnextContext(2);
    const _listType_r7 = ɵɵreadContextLet(0);
    ɵɵconditional(_listType_r7 !== "picture-card" ? 0 : -1);
  }
}
function NzUploadListComponent_For_2_ng_template_12_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    const _r12 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "a", 30);
    ɵɵlistener("click", function NzUploadListComponent_For_2_ng_template_12_Conditional_0_Template_a_click_0_listener($event) {
      ɵɵrestoreView(_r12);
      const file_r3 = ɵɵnextContext(2).$implicit;
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.handlePreview(file_r3, $event));
    });
    ɵɵtext(1);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(2).$implicit;
    ɵɵproperty("href", file_r3.url, ɵɵsanitizeUrl);
    ɵɵattribute("title", file_r3.name)("download", file_r3.linkProps && file_r3.linkProps.download);
    ɵɵadvance();
    ɵɵtextInterpolate1(" ", file_r3.name, " ");
  }
}
function NzUploadListComponent_For_2_ng_template_12_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r13 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "span", 31);
    ɵɵlistener("click", function NzUploadListComponent_For_2_ng_template_12_Conditional_1_Template_span_click_0_listener($event) {
      ɵɵrestoreView(_r13);
      const file_r3 = ɵɵnextContext(2).$implicit;
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.handlePreview(file_r3, $event));
    });
    ɵɵtext(1);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(2).$implicit;
    ɵɵattribute("title", file_r3.name);
    ɵɵadvance();
    ɵɵtextInterpolate1(" ", file_r3.name, " ");
  }
}
function NzUploadListComponent_For_2_ng_template_12_ng_template_2_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_12_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_ng_template_12_Conditional_0_Template, 2, 4, "a", 28)(1, NzUploadListComponent_For_2_ng_template_12_Conditional_1_Template, 2, 2, "span", 29);
    ɵɵtemplate(2, NzUploadListComponent_For_2_ng_template_12_ng_template_2_Template, 0, 0, "ng-template", 10);
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext().$implicit;
    const downloadOrDelete_r14 = ɵɵreference(11);
    ɵɵconditional(file_r3.url ? 0 : 1);
    ɵɵadvance(2);
    ɵɵproperty("ngTemplateOutlet", downloadOrDelete_r14);
  }
}
function NzUploadListComponent_For_2_ng_template_16_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_ng_template_17_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_Conditional_18_Conditional_0_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    const _r15 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "a", 34);
    ɵɵlistener("click", function NzUploadListComponent_For_2_Conditional_18_Conditional_0_Conditional_1_Template_a_click_0_listener($event) {
      ɵɵrestoreView(_r15);
      const file_r3 = ɵɵnextContext(3).$implicit;
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.handlePreview(file_r3, $event));
    });
    ɵɵelement(1, "nz-icon", 35);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(3).$implicit;
    const ctx_r1 = ɵɵnextContext();
    ɵɵstyleMap(!(file_r3.url || file_r3.thumbUrl) ? ɵɵpureFunction0(4, _c4) : null);
    ɵɵproperty("href", file_r3.url || file_r3.thumbUrl, ɵɵsanitizeUrl);
    ɵɵattribute("title", ctx_r1.locale.previewFile);
  }
}
function NzUploadListComponent_For_2_Conditional_18_Conditional_0_Conditional_2_ng_template_0_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_Conditional_18_Conditional_0_Conditional_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzUploadListComponent_For_2_Conditional_18_Conditional_0_Conditional_2_ng_template_0_Template, 0, 0, "ng-template", 10);
  }
  if (rf & 2) {
    ɵɵnextContext(3);
    const downloadIcon_r11 = ɵɵreference(9);
    ɵɵproperty("ngTemplateOutlet", downloadIcon_r11);
  }
}
function NzUploadListComponent_For_2_Conditional_18_Conditional_0_ng_template_3_Template(rf, ctx) {
}
function NzUploadListComponent_For_2_Conditional_18_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "span", 32);
    ɵɵconditionalCreate(1, NzUploadListComponent_For_2_Conditional_18_Conditional_0_Conditional_1_Template, 2, 5, "a", 33);
    ɵɵconditionalCreate(2, NzUploadListComponent_For_2_Conditional_18_Conditional_0_Conditional_2_Template, 1, 1, null, 10);
    ɵɵtemplate(3, NzUploadListComponent_For_2_Conditional_18_Conditional_0_ng_template_3_Template, 0, 0, "ng-template", 10);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext(2).$implicit;
    const removeIcon_r10 = ɵɵreference(7);
    const ctx_r1 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵconditional(ctx_r1.icons.showPreviewIcon ? 1 : -1);
    ɵɵadvance();
    ɵɵconditional(file_r3.status === "done" ? 2 : -1);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", removeIcon_r10);
  }
}
function NzUploadListComponent_For_2_Conditional_18_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadListComponent_For_2_Conditional_18_Conditional_0_Template, 4, 3, "span", 32);
  }
  if (rf & 2) {
    ɵɵnextContext(2);
    const _listType_r7 = ɵɵreadContextLet(0);
    ɵɵconditional(_listType_r7 === "picture-card" ? 0 : -1);
  }
}
function NzUploadListComponent_For_2_Conditional_19_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 11);
    ɵɵelement(1, "nz-progress", 36);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const file_r3 = ɵɵnextContext().$implicit;
    ɵɵadvance();
    ɵɵproperty("nzPercent", file_r3.percent)("nzShowInfo", false)("nzStrokeWidth", 2);
  }
}
function NzUploadListComponent_For_2_Template(rf, ctx) {
  if (rf & 1) {
    const _r1 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div")(1, "div", 7);
    ɵɵanimateLeave(function NzUploadListComponent_For_2_Template_animateleave_cb() {
      ɵɵrestoreView(_r1);
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.itemAnimationLeave());
    });
    ɵɵanimateEnter(function NzUploadListComponent_For_2_Template_animateenter_cb() {
      ɵɵrestoreView(_r1);
      const ctx_r1 = ɵɵnextContext();
      return ɵɵresetView(ctx_r1.itemAnimationEnter());
    });
    ɵɵtemplate(2, NzUploadListComponent_For_2_ng_template_2_Template, 3, 1, "ng-template", null, 0, ɵɵtemplateRefExtractor)(4, NzUploadListComponent_For_2_ng_template_4_Template, 2, 1, "ng-template", null, 1, ɵɵtemplateRefExtractor)(6, NzUploadListComponent_For_2_ng_template_6_Template, 1, 1, "ng-template", null, 2, ɵɵtemplateRefExtractor)(8, NzUploadListComponent_For_2_ng_template_8_Template, 1, 1, "ng-template", null, 3, ɵɵtemplateRefExtractor)(10, NzUploadListComponent_For_2_ng_template_10_Template, 1, 1, "ng-template", null, 4, ɵɵtemplateRefExtractor)(12, NzUploadListComponent_For_2_ng_template_12_Template, 3, 2, "ng-template", null, 5, ɵɵtemplateRefExtractor);
    ɵɵelementStart(14, "div", 8)(15, "span", 9);
    ɵɵtemplate(16, NzUploadListComponent_For_2_ng_template_16_Template, 0, 0, "ng-template", 10)(17, NzUploadListComponent_For_2_ng_template_17_Template, 0, 0, "ng-template", 10);
    ɵɵelementEnd()();
    ɵɵconditionalCreate(18, NzUploadListComponent_For_2_Conditional_18_Template, 1, 1)(19, NzUploadListComponent_For_2_Conditional_19_Template, 2, 3, "div", 11);
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const file_r3 = ctx.$implicit;
    const icon_r16 = ɵɵreference(3);
    const preview_r17 = ɵɵreference(13);
    ɵɵnextContext();
    const _listType_r7 = ɵɵreadContextLet(0);
    ɵɵclassMap(ɵɵinterpolate1("ant-upload-list-", _listType_r7, "-container"));
    ɵɵadvance();
    ɵɵclassMap(ɵɵinterpolate2("ant-upload-list-item ant-upload-list-item-", file_r3.status, " ant-upload-list-item-list-type-", _listType_r7));
    ɵɵproperty("nzTooltipTitle", file_r3.status === "error" ? file_r3.message : null);
    ɵɵattribute("data-key", file_r3.key);
    ɵɵadvance(15);
    ɵɵproperty("ngTemplateOutlet", icon_r16);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", preview_r17);
    ɵɵadvance();
    ɵɵconditional(!file_r3.isUploading ? 18 : 19);
  }
}
var _c5 = ["uploadComp"];
var _c6 = ["listComp"];
var _c7 = () => [];
function NzUploadComponent_ng_template_0_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-upload-list", 6, 3);
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext(2);
    ɵɵstyleProp("display", ctx_r0.nzShowUploadList ? "" : "none");
    ɵɵproperty("locale", ctx_r0.locale)("listType", ctx_r0.nzListType)("items", ctx_r0.nzFileList || ɵɵpureFunction0(12, _c7))("icons", ctx_r0.nzShowUploadList)("iconRender", ctx_r0.nzIconRender)("previewFile", ctx_r0.nzPreviewFile)("previewIsImage", ctx_r0.nzPreviewIsImage)("onPreview", ctx_r0.nzPreview)("onRemove", ctx_r0.onRemove)("onDownload", ctx_r0.nzDownload);
  }
}
function NzUploadComponent_ng_template_0_Conditional_1_ng_container_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainer(0);
  }
}
function NzUploadComponent_ng_template_0_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzUploadComponent_ng_template_0_Conditional_1_ng_container_0_Template, 1, 0, "ng-container", 7);
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext(2);
    ɵɵproperty("ngTemplateOutlet", ctx_r0.nzFileListRender)("ngTemplateOutletContext", ɵɵpureFunction1(2, _c3, ctx_r0.nzFileList));
  }
}
function NzUploadComponent_ng_template_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadComponent_ng_template_0_Conditional_0_Template, 2, 13, "nz-upload-list", 5);
    ɵɵconditionalCreate(1, NzUploadComponent_ng_template_0_Conditional_1_Template, 1, 4, "ng-container");
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    ɵɵconditional(ctx_r0.locale && !ctx_r0.nzFileListRender ? 0 : -1);
    ɵɵadvance();
    ɵɵconditional(ctx_r0.nzFileListRender ? 1 : -1);
  }
}
function NzUploadComponent_ng_template_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵprojection(0);
  }
}
function NzUploadComponent_ng_template_4_ng_template_3_Template(rf, ctx) {
}
function NzUploadComponent_ng_template_4_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div")(1, "div", 8, 4);
    ɵɵtemplate(3, NzUploadComponent_ng_template_4_ng_template_3_Template, 0, 0, "ng-template", 9);
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    const con_r2 = ɵɵreference(3);
    ɵɵclassMap(ctx_r0.classList());
    ɵɵstyleProp("display", ctx_r0.nzShowButton ? "" : "none");
    ɵɵclassProp("ant-upload-rtl", ctx_r0.dir() === "rtl");
    ɵɵadvance();
    ɵɵproperty("options", ctx_r0._btnOptions);
    ɵɵadvance(2);
    ɵɵproperty("ngTemplateOutlet", con_r2);
  }
}
function NzUploadComponent_Conditional_6_ng_template_4_Template(rf, ctx) {
}
function NzUploadComponent_Conditional_6_ng_template_5_Template(rf, ctx) {
}
function NzUploadComponent_Conditional_6_Template(rf, ctx) {
  if (rf & 1) {
    const _r3 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 10);
    ɵɵlistener("drop", function NzUploadComponent_Conditional_6_Template_div_drop_0_listener($event) {
      ɵɵrestoreView(_r3);
      const ctx_r0 = ɵɵnextContext();
      return ɵɵresetView(ctx_r0.fileDrop($event));
    })("dragover", function NzUploadComponent_Conditional_6_Template_div_dragover_0_listener($event) {
      ɵɵrestoreView(_r3);
      const ctx_r0 = ɵɵnextContext();
      return ɵɵresetView(ctx_r0.fileDrop($event));
    })("dragleave", function NzUploadComponent_Conditional_6_Template_div_dragleave_0_listener($event) {
      ɵɵrestoreView(_r3);
      const ctx_r0 = ɵɵnextContext();
      return ɵɵresetView(ctx_r0.fileDrop($event));
    });
    ɵɵelementStart(1, "div", 11, 4)(3, "div", 12);
    ɵɵtemplate(4, NzUploadComponent_Conditional_6_ng_template_4_Template, 0, 0, "ng-template", 9);
    ɵɵelementEnd()()();
    ɵɵtemplate(5, NzUploadComponent_Conditional_6_ng_template_5_Template, 0, 0, "ng-template", 9);
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    const list_r4 = ɵɵreference(1);
    const con_r2 = ɵɵreference(3);
    ɵɵclassMap(ctx_r0.classList());
    ɵɵclassProp("ant-upload-rtl", ctx_r0.dir() === "rtl");
    ɵɵadvance();
    ɵɵproperty("options", ctx_r0._btnOptions);
    ɵɵadvance(3);
    ɵɵproperty("ngTemplateOutlet", con_r2);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", list_r4);
  }
}
function NzUploadComponent_Conditional_7_Conditional_0_ng_template_0_Template(rf, ctx) {
}
function NzUploadComponent_Conditional_7_Conditional_0_ng_template_1_Template(rf, ctx) {
}
function NzUploadComponent_Conditional_7_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzUploadComponent_Conditional_7_Conditional_0_ng_template_0_Template, 0, 0, "ng-template", 9)(1, NzUploadComponent_Conditional_7_Conditional_0_ng_template_1_Template, 0, 0, "ng-template", 9);
  }
  if (rf & 2) {
    ɵɵnextContext(2);
    const list_r4 = ɵɵreference(1);
    const btn_r5 = ɵɵreference(5);
    ɵɵproperty("ngTemplateOutlet", list_r4);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", btn_r5);
  }
}
function NzUploadComponent_Conditional_7_Conditional_1_ng_template_0_Template(rf, ctx) {
}
function NzUploadComponent_Conditional_7_Conditional_1_ng_template_1_Template(rf, ctx) {
}
function NzUploadComponent_Conditional_7_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzUploadComponent_Conditional_7_Conditional_1_ng_template_0_Template, 0, 0, "ng-template", 9)(1, NzUploadComponent_Conditional_7_Conditional_1_ng_template_1_Template, 0, 0, "ng-template", 9);
  }
  if (rf & 2) {
    ɵɵnextContext(2);
    const list_r4 = ɵɵreference(1);
    const btn_r5 = ɵɵreference(5);
    ɵɵproperty("ngTemplateOutlet", btn_r5);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", list_r4);
  }
}
function NzUploadComponent_Conditional_7_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzUploadComponent_Conditional_7_Conditional_0_Template, 2, 2)(1, NzUploadComponent_Conditional_7_Conditional_1_Template, 2, 2);
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    ɵɵconditional(ctx_r0.nzListType === "picture-card" ? 0 : 1);
  }
}
var NzUploadBtnComponent = class _NzUploadBtnComponent {
  reqs = {};
  destroyed = false;
  file;
  options;
  onClick() {
    if (this.options.disabled || !this.options.openFileDialogOnClick) {
      return;
    }
    this.file.nativeElement.click();
  }
  // skip safari bug
  onFileDrop(e) {
    if (this.options.disabled || e.type === "dragover") {
      e.preventDefault();
      return;
    }
    if (this.options.directory) {
      this.traverseFileTree(e.dataTransfer.items);
    } else {
      const files = Array.prototype.slice.call(e.dataTransfer.files).filter((file) => this.attrAccept(file, this.options.accept));
      if (files.length) {
        this.uploadFiles(files);
      }
    }
    e.preventDefault();
  }
  onChange(e) {
    if (this.options.disabled) {
      return;
    }
    const hie = e.target;
    this.uploadFiles(hie.files);
    hie.value = "";
  }
  traverseFileTree(files) {
    const _traverseFileTree = (item, path) => {
      if (item.isFile) {
        item.file((file) => {
          if (this.attrAccept(file, this.options.accept)) {
            this.uploadFiles([file]);
          }
        });
      } else if (item.isDirectory) {
        const dirReader = item.createReader();
        dirReader.readEntries((entries) => {
          for (const entrieItem of entries) {
            _traverseFileTree(entrieItem, `${path}${item.name}/`);
          }
        });
      }
    };
    for (const file of files) {
      _traverseFileTree(file.webkitGetAsEntry(), "");
    }
  }
  attrAccept(file, acceptedFiles) {
    if (file && acceptedFiles) {
      const acceptedFilesArray = Array.isArray(acceptedFiles) ? acceptedFiles : acceptedFiles.split(",");
      const fileName = `${file.name}`;
      const mimeType = `${file.type}`;
      const baseMimeType = mimeType.replace(/\/.*$/, "");
      return acceptedFilesArray.some((type) => {
        const validType = type.trim();
        if (validType.charAt(0) === ".") {
          return fileName.toLowerCase().indexOf(validType.toLowerCase(), fileName.toLowerCase().length - validType.toLowerCase().length) !== -1;
        } else if (/\/\*$/.test(validType)) {
          return baseMimeType === validType.replace(/\/.*$/, "");
        }
        return mimeType === validType;
      });
    }
    return true;
  }
  attachUid(file) {
    if (!file.uid) {
      file.uid = Math.random().toString(36).substring(2);
    }
    return file;
  }
  uploadFiles(fileList) {
    let filters$ = of(Array.prototype.slice.call(fileList));
    if (this.options.filters) {
      this.options.filters.forEach((f) => {
        filters$ = filters$.pipe(switchMap((list) => {
          const fnRes = f.fn(list);
          return fnRes instanceof Observable ? fnRes : of(fnRes);
        }));
      });
    }
    filters$.subscribe({
      next: (list) => {
        list.forEach((file) => {
          this.attachUid(file);
          this.upload(file, list);
        });
      },
      error: (e) => {
        warn(`Unhandled upload filter error`, e);
      }
    });
  }
  upload(file, fileList) {
    if (!this.options.beforeUpload) {
      return this.post(file);
    }
    const before = this.options.beforeUpload(file, fileList);
    const successBeforeLoadHook = (processedFile) => {
      const processedFileType = Object.prototype.toString.call(processedFile);
      if (typeof processedFile !== "boolean" && (processedFileType === "[object File]" || processedFileType === "[object Blob]")) {
        processedFile.uid = file.uid;
        this.post(file, processedFile);
      } else if (processedFile) {
        this.post(file);
      }
    };
    const errorBeforeLoadHook = (error) => {
      warn(`Unhandled upload beforeUpload error`, error);
    };
    if (before instanceof Observable) {
      before.subscribe({
        next: successBeforeLoadHook,
        error: errorBeforeLoadHook
      });
    } else if (before instanceof Promise) {
      before.then(successBeforeLoadHook).catch(errorBeforeLoadHook);
    } else if (before) {
      return this.post(file);
    }
  }
  post(file, processedFile) {
    if (this.destroyed) {
      return;
    }
    let process$ = of(processedFile || file);
    let transformedFile;
    const opt = this.options;
    const {
      uid
    } = file;
    const {
      action,
      data,
      headers,
      transformFile
    } = opt;
    const args = {
      action: typeof action === "string" ? action : "",
      name: opt.name,
      headers,
      file,
      postFile: file,
      data,
      withCredentials: opt.withCredentials,
      onProgress: opt.onProgress ? (e) => {
        opt.onProgress(e, file);
      } : void 0,
      onSuccess: (ret, xhr) => {
        this.clean(uid);
        opt.onSuccess(ret, file, xhr);
      },
      onError: (xhr) => {
        this.clean(uid);
        opt.onError(xhr, file);
      }
    };
    if (typeof action === "function") {
      const actionResult = action(file);
      if (actionResult instanceof Observable) {
        process$ = process$.pipe(switchMap(() => actionResult), map((res) => {
          args.action = res;
          return file;
        }));
      } else {
        args.action = actionResult;
      }
    }
    if (typeof transformFile === "function") {
      const transformResult = transformFile(file);
      process$ = process$.pipe(switchMap(() => transformResult instanceof Observable ? transformResult : of(transformResult)), tap((newFile) => transformedFile = newFile));
    }
    if (typeof data === "function") {
      const dataResult = data(file);
      if (dataResult instanceof Observable) {
        process$ = process$.pipe(
          /**
           * this is a little bit tricky but here is the explanation:
           * Potentially, people can use the `beforeUpload` hook to transform the file, and also `nzTransformFile` hook to transform the file,
           * if beforeUpload hook transform the file, so nzTransformFile hook must not be called, otherwise the file will be transformed twice
           * Normally this can not happen, but it is possible until we remove the `nzTransformFile` hook
           */
          filter(() => !processedFile),
          switchMap(() => dataResult),
          map((res) => {
            args.data = res;
            return transformedFile ?? file;
          })
        );
      } else {
        args.data = dataResult;
      }
    }
    if (typeof headers === "function") {
      const headersResult = headers(file);
      if (headersResult instanceof Observable) {
        process$ = process$.pipe(switchMap(() => headersResult), map((res) => {
          args.headers = res;
          return transformedFile ?? file;
        }));
      } else {
        args.headers = headersResult;
      }
    }
    process$.subscribe((newFile) => {
      args.postFile = newFile;
      const req$ = (opt.customRequest || this.xhr).call(this, args);
      if (!(req$ instanceof Subscription)) {
        warn(`Must return Subscription type in '[nzCustomRequest]' property`);
      }
      this.reqs[uid] = req$;
      opt.onStart(file);
    });
  }
  xhr(args) {
    const formData = new FormData();
    if (args.data) {
      Object.keys(args.data).map((key) => {
        formData.append(key, args.data[key]);
      });
    }
    formData.append(args.name, args.postFile);
    if (!args.headers) {
      args.headers = {};
    }
    if (args.headers["X-Requested-With"] !== null) {
      args.headers["X-Requested-With"] = `XMLHttpRequest`;
    } else {
      delete args.headers["X-Requested-With"];
    }
    const req = new HttpRequest("POST", args.action, formData, {
      reportProgress: true,
      withCredentials: args.withCredentials,
      headers: new HttpHeaders(args.headers)
    });
    return this.http.request(req).subscribe({
      next: (event) => {
        if (event.type === HttpEventType.UploadProgress) {
          if (event.total > 0) {
            event.percent = event.loaded / event.total * 100;
          }
          args.onProgress(event, args.file);
        } else if (event instanceof HttpResponse) {
          args.onSuccess(event.body, args.file, event);
        }
      },
      error: (err) => {
        this.abort(args.file);
        args.onError(err, args.file);
      }
    });
  }
  clean(uid) {
    const req$ = this.reqs[uid];
    if (req$ instanceof Subscription) {
      req$.unsubscribe();
    }
    delete this.reqs[uid];
  }
  abort(file) {
    if (file) {
      this.clean(file && file.uid);
    } else {
      Object.keys(this.reqs).forEach((uid) => this.clean(uid));
    }
  }
  http = inject(HttpClient, {
    optional: true
  });
  elementRef = inject(ElementRef);
  destroyRef = inject(DestroyRef);
  constructor() {
    if (!this.http) {
      throw new Error(`Not found 'HttpClient', You can configure 'HttpClient' with 'provideHttpClient()' in your root module.`);
    }
    this.destroyRef.onDestroy(() => {
      this.destroyed = true;
      this.abort();
    });
  }
  ngOnInit() {
    fromEventOutsideAngular(this.elementRef.nativeElement, "click").pipe(takeUntilDestroyed(this.destroyRef)).subscribe(() => this.onClick());
    fromEventOutsideAngular(this.elementRef.nativeElement, "keydown").pipe(takeUntilDestroyed(this.destroyRef)).subscribe((event) => {
      if (this.options.disabled) {
        return;
      }
      if (event.key === "Enter" || event.keyCode === ENTER) {
        this.onClick();
      }
    });
  }
  static ɵfac = function NzUploadBtnComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzUploadBtnComponent)();
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _NzUploadBtnComponent,
    selectors: [["", "nz-upload-btn", ""]],
    viewQuery: function NzUploadBtnComponent_Query(rf, ctx) {
      if (rf & 1) {
        ɵɵviewQuery(_c0, 7);
      }
      if (rf & 2) {
        let _t;
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.file = _t.first);
      }
    },
    hostAttrs: [1, "ant-upload"],
    hostVars: 4,
    hostBindings: function NzUploadBtnComponent_HostBindings(rf, ctx) {
      if (rf & 1) {
        ɵɵlistener("drop", function NzUploadBtnComponent_drop_HostBindingHandler($event) {
          return ctx.onFileDrop($event);
        })("dragover", function NzUploadBtnComponent_dragover_HostBindingHandler($event) {
          return ctx.onFileDrop($event);
        });
      }
      if (rf & 2) {
        ɵɵattribute("tabindex", "0")("role", "button");
        ɵɵclassProp("ant-upload-disabled", ctx.options.disabled);
      }
    },
    inputs: {
      options: "options"
    },
    exportAs: ["nzUploadBtn"],
    attrs: _c1,
    ngContentSelectors: _c2,
    decls: 3,
    vars: 6,
    consts: [["file", ""], ["type", "file", 3, "change", "multiple"]],
    template: function NzUploadBtnComponent_Template(rf, ctx) {
      if (rf & 1) {
        const _r1 = ɵɵgetCurrentView();
        ɵɵprojectionDef();
        ɵɵdomElementStart(0, "input", 1, 0);
        ɵɵdomListener("change", function NzUploadBtnComponent_Template_input_change_0_listener($event) {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.onChange($event));
        });
        ɵɵdomElementEnd();
        ɵɵprojection(2);
      }
      if (rf & 2) {
        ɵɵstyleProp("display", "none");
        ɵɵdomProperty("multiple", ctx.options.multiple);
        ɵɵattribute("accept", ctx.options.accept)("directory", ctx.options.directory ? "directory" : null)("webkitdirectory", ctx.options.directory ? "webkitdirectory" : null);
      }
    },
    encapsulation: 2
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzUploadBtnComponent, [{
    type: Component,
    args: [{
      selector: "[nz-upload-btn]",
      exportAs: "nzUploadBtn",
      host: {
        class: "ant-upload",
        "[attr.tabindex]": '"0"',
        "[attr.role]": '"button"',
        "[class.ant-upload-disabled]": "options.disabled",
        "(drop)": "onFileDrop($event)",
        "(dragover)": "onFileDrop($event)"
      },
      encapsulation: ViewEncapsulation.None,
      template: `<!--
  We explicitly bind \`style.display\` to avoid using an inline style
  attribute property (which is not allowed when CSP \`unsafe-inline\`
  is not specified).
-->
<input
  type="file"
  #file
  (change)="onChange($event)"
  [attr.accept]="options.accept"
  [attr.directory]="options.directory ? 'directory' : null"
  [attr.webkitdirectory]="options.directory ? 'webkitdirectory' : null"
  [multiple]="options.multiple"
  [style.display]="'none'"
/>
<ng-content></ng-content>
`
    }]
  }], () => [], {
    file: [{
      type: ViewChild,
      args: ["file", {
        static: true
      }]
    }],
    options: [{
      type: Input
    }]
  });
})();
var isImageFileType = (type) => !!type && type.indexOf("image/") === 0;
var CLASS_NAME$1 = "ant-upload-list";
var MEASURE_SIZE = 200;
var NzUploadListComponent = class _NzUploadListComponent {
  list = [];
  listType = input("text", ...ngDevMode ? [{
    debugName: "listType"
  }] : []);
  locale = {};
  set items(list) {
    this.list = list;
  }
  icons;
  onPreview;
  onRemove;
  onDownload;
  previewFile;
  previewIsImage;
  iconRender = null;
  document = inject(DOCUMENT);
  destroyRef = inject(DestroyRef);
  ngZone = inject(NgZone);
  cdr = inject(ChangeDetectorRef);
  platform = inject(Platform);
  dir = inject(Directionality).valueSignal;
  class = computed(() => {
    const cls = [CLASS_NAME$1, this.generateClass(this.listType())];
    if (this.dir() === "rtl") {
      cls.push(this.generateClass("rtl"));
    }
    return cls;
  }, ...ngDevMode ? [{
    debugName: "class"
  }] : []);
  showPic = computed(() => {
    return this.listType() === "picture" || this.listType() === "picture-card";
  }, ...ngDevMode ? [{
    debugName: "showPic"
  }] : []);
  itemAnimationEnter = withAnimationCheck(() => `ant-upload-${this.showPic() ? "animate-inline" : "animate"}-enter`);
  itemAnimationLeave = withAnimationCheck(() => `ant-upload-${this.showPic() ? "animate-inline" : "animate"}-leave`);
  genErr(file) {
    if (file.response && typeof file.response === "string") {
      return file.response;
    }
    return file.error && file.error.statusText || this.locale.uploadError;
  }
  extname(url) {
    const temp = url.split("/");
    const filename = temp[temp.length - 1];
    const filenameWithoutSuffix = filename.split(/#|\?/)[0];
    return (/\.[^./\\]*$/.exec(filenameWithoutSuffix) || [""])[0];
  }
  isImageUrl(file) {
    if (isImageFileType(file.type)) {
      return true;
    }
    const url = file.thumbUrl || file.url || "";
    if (!url) {
      return false;
    }
    const extension = this.extname(url);
    if (/^data:image\//.test(url) || /(webp|svg|png|gif|jpg|jpeg|jfif|bmp|dpg)$/i.test(extension)) {
      return true;
    } else if (/^data:/.test(url)) {
      return false;
    } else if (extension) {
      return false;
    }
    return true;
  }
  getIconType(file) {
    if (!this.showPic()) {
      return "";
    }
    if (file.isUploading || !file.thumbUrl && !file.url) {
      return "uploading";
    } else {
      return "thumbnail";
    }
  }
  previewImage(file) {
    if (!isImageFileType(file.type) || !this.platform.isBrowser) {
      return of("");
    }
    const canvas = this.document.createElement("canvas");
    canvas.width = MEASURE_SIZE;
    canvas.height = MEASURE_SIZE;
    canvas.style.cssText = `position: fixed; left: 0; top: 0; width: ${MEASURE_SIZE}px; height: ${MEASURE_SIZE}px; z-index: 9999; display: none;`;
    this.document.body.appendChild(canvas);
    const ctx = canvas.getContext("2d");
    const img = new Image();
    const objectUrl = URL.createObjectURL(file);
    img.src = objectUrl;
    return fromEventOutsideAngular(img, "load").pipe(map(() => {
      const {
        width,
        height
      } = img;
      let drawWidth = MEASURE_SIZE;
      let drawHeight = MEASURE_SIZE;
      let offsetX = 0;
      let offsetY = 0;
      if (width < height) {
        drawHeight = height * (MEASURE_SIZE / width);
        offsetY = -(drawHeight - drawWidth) / 2;
      } else {
        drawWidth = width * (MEASURE_SIZE / height);
        offsetX = -(drawWidth - drawHeight) / 2;
      }
      try {
        ctx.drawImage(img, offsetX, offsetY, drawWidth, drawHeight);
      } catch {
      }
      const dataURL = canvas.toDataURL();
      this.document.body.removeChild(canvas);
      URL.revokeObjectURL(objectUrl);
      return dataURL;
    }));
  }
  genThumb() {
    if (!this.platform.isBrowser) {
      return;
    }
    const win = window;
    if (!this.showPic() || typeof document === "undefined" || typeof win === "undefined" || !win.FileReader || !win.File) {
      return;
    }
    this.list.filter((file) => file.originFileObj instanceof File && file.thumbUrl === void 0).forEach((file) => {
      file.thumbUrl = "";
      const dataUrl$ = (this.previewFile ? this.previewFile(file) : this.previewImage(file.originFileObj)).pipe(takeUntilDestroyed(this.destroyRef));
      this.ngZone.runOutsideAngular(() => {
        dataUrl$.subscribe((dataUrl) => {
          this.ngZone.run(() => {
            file.thumbUrl = dataUrl;
            this.detectChanges();
          });
        });
      });
    });
  }
  showDownload(file) {
    return !!(this.icons.showDownloadIcon && file.status === "done");
  }
  fixData() {
    this.list.forEach((file) => {
      file.isUploading = file.status === "uploading";
      file.message = this.genErr(file);
      file.linkProps = typeof file.linkProps === "string" ? JSON.parse(file.linkProps) : file.linkProps;
      file.isImageUrl = this.previewIsImage ? this.previewIsImage(file) : this.isImageUrl(file);
      file.iconType = this.getIconType(file);
      file.showDownload = this.showDownload(file);
    });
  }
  handlePreview(file, e) {
    if (!this.onPreview) {
      return;
    }
    e.preventDefault();
    return this.onPreview(file);
  }
  handleRemove(file, e) {
    e.preventDefault();
    if (this.onRemove) {
      this.onRemove(file);
    }
  }
  handleDownload(file) {
    if (typeof this.onDownload === "function") {
      this.onDownload(file);
    } else if (file.url) {
      window.open(file.url);
    }
  }
  detectChanges() {
    this.fixData();
    this.cdr.detectChanges();
  }
  ngOnChanges() {
    this.fixData();
    this.genThumb();
  }
  generateClass(suffix) {
    return generateClassName(CLASS_NAME$1, suffix);
  }
  static ɵfac = function NzUploadListComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzUploadListComponent)();
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _NzUploadListComponent,
    selectors: [["nz-upload-list"]],
    hostVars: 2,
    hostBindings: function NzUploadListComponent_HostBindings(rf, ctx) {
      if (rf & 2) {
        ɵɵclassMap(ctx.class());
      }
    },
    inputs: {
      listType: [1, "listType"],
      locale: "locale",
      items: "items",
      icons: "icons",
      onPreview: "onPreview",
      onRemove: "onRemove",
      onDownload: "onDownload",
      previewFile: "previewFile",
      previewIsImage: "previewIsImage",
      iconRender: "iconRender"
    },
    exportAs: ["nzUploadList"],
    features: [ɵɵNgOnChangesFeature],
    decls: 3,
    vars: 1,
    consts: [["icon", ""], ["iconNode", ""], ["removeIcon", ""], ["downloadIcon", ""], ["downloadOrDelete", ""], ["preview", ""], [3, "class"], ["nz-tooltip", "", 3, "nzTooltipTitle"], [1, "ant-upload-list-item-info"], [1, "ant-upload-span"], [3, "ngTemplateOutlet"], [1, "ant-upload-list-item-progress"], [1, "ant-upload-list-item-thumbnail", 3, "ant-upload-list-item-file"], ["target", "_blank", "rel", "noopener noreferrer", 1, "ant-upload-list-item-thumbnail", 3, "ant-upload-list-item-file", "href"], [1, "ant-upload-text-icon"], [1, "ant-upload-list-item-thumbnail"], [3, "ngTemplateOutlet", "ngTemplateOutletContext"], ["target", "_blank", "rel", "noopener noreferrer", 1, "ant-upload-list-item-thumbnail", 3, "click", "href"], [1, "ant-upload-list-item-image", 3, "src"], [3, "nzType"], ["nzType", "loading"], ["nzTheme", "twotone", 3, "nzType"], ["type", "button", "nz-button", "", "nzType", "text", "nzSize", "small", 1, "ant-upload-list-item-card-actions-btn"], ["type", "button", "nz-button", "", "nzType", "text", "nzSize", "small", 1, "ant-upload-list-item-card-actions-btn", 3, "click"], ["nzType", "delete"], ["nzType", "download"], [1, "ant-upload-list-item-card-actions", 3, "picture"], [1, "ant-upload-list-item-card-actions"], ["target", "_blank", "rel", "noopener noreferrer", 1, "ant-upload-list-item-name", 3, "href"], [1, "ant-upload-list-item-name"], ["target", "_blank", "rel", "noopener noreferrer", 1, "ant-upload-list-item-name", 3, "click", "href"], [1, "ant-upload-list-item-name", 3, "click"], [1, "ant-upload-list-item-actions"], ["target", "_blank", "rel", "noopener noreferrer", 3, "href", "style"], ["target", "_blank", "rel", "noopener noreferrer", 3, "click", "href"], ["nzType", "eye"], ["nzType", "line", 3, "nzPercent", "nzShowInfo", "nzStrokeWidth"]],
    template: function NzUploadListComponent_Template(rf, ctx) {
      if (rf & 1) {
        ɵɵdeclareLet(0);
        ɵɵrepeaterCreate(1, NzUploadListComponent_For_2_Template, 20, 12, "div", 6, ɵɵrepeaterTrackByIdentity);
      }
      if (rf & 2) {
        ɵɵstoreLet(ctx.listType());
        ɵɵadvance();
        ɵɵrepeater(ctx.list);
      }
    },
    dependencies: [NzTooltipModule, NzTooltipDirective, NgTemplateOutlet, NzIconModule, NzIconDirective, NzButtonModule, NzButtonComponent, NzTransitionPatchDirective, NzProgressModule, NzProgressComponent],
    encapsulation: 2,
    changeDetection: 0
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzUploadListComponent, [{
    type: Component,
    args: [{
      selector: "nz-upload-list",
      exportAs: "nzUploadList",
      host: {
        "[class]": "class()"
      },
      encapsulation: ViewEncapsulation.None,
      changeDetection: ChangeDetectionStrategy.OnPush,
      imports: [NzTooltipModule, NgTemplateOutlet, NzIconModule, NzButtonModule, NzProgressModule],
      template: `@let _listType = listType();
@for (file of list; track file) {
  <div class="ant-upload-list-{{ _listType }}-container">
    <div
      class="ant-upload-list-item ant-upload-list-item-{{ file.status }} ant-upload-list-item-list-type-{{ _listType }}"
      [attr.data-key]="file.key"
      [animate.enter]="itemAnimationEnter()"
      [animate.leave]="itemAnimationLeave()"
      nz-tooltip
      [nzTooltipTitle]="file.status === 'error' ? file.message : null"
    >
      <ng-template #icon>
        @switch (file.iconType) {
          @case ('uploading') {
            <div class="ant-upload-list-item-thumbnail" [class.ant-upload-list-item-file]="!file.isUploading">
              <ng-template [ngTemplateOutlet]="iconNode" [ngTemplateOutletContext]="{ $implicit: file }"></ng-template>
            </div>
          }
          @case ('thumbnail') {
            <a
              class="ant-upload-list-item-thumbnail"
              [class.ant-upload-list-item-file]="!file.isImageUrl"
              target="_blank"
              rel="noopener noreferrer"
              [href]="file.url || file.thumbUrl"
              (click)="handlePreview(file, $event)"
            >
              @if (file.isImageUrl) {
                <img class="ant-upload-list-item-image" [src]="file.thumbUrl || file.url" [attr.alt]="file.name" />
              } @else {
                <ng-template
                  [ngTemplateOutlet]="iconNode"
                  [ngTemplateOutletContext]="{ $implicit: file }"
                ></ng-template>
              }
            </a>
          }
          @default {
            <div class="ant-upload-text-icon">
              <ng-template [ngTemplateOutlet]="iconNode" [ngTemplateOutletContext]="{ $implicit: file }"></ng-template>
            </div>
          }
        }
      </ng-template>

      <ng-template #iconNode let-file>
        @if (!iconRender) {
          @switch (_listType) {
            @case ('picture') {
              @if (file.isUploading) {
                <nz-icon nzType="loading" />
              } @else {
                <nz-icon [nzType]="file.isImageUrl ? 'picture' : 'file'" nzTheme="twotone" />
              }
            }
            @case ('picture-card') {
              @if (file.isUploading) {
                {{ locale.uploading }}
              } @else {
                <nz-icon [nzType]="file.isImageUrl ? 'picture' : 'file'" nzTheme="twotone" />
              }
            }
            @default {
              <nz-icon [nzType]="file.isUploading ? 'loading' : 'paper-clip'" />
            }
          }
        } @else {
          <ng-template [ngTemplateOutlet]="iconRender" [ngTemplateOutletContext]="{ $implicit: file }"></ng-template>
        }
      </ng-template>

      <ng-template #removeIcon>
        @if (icons.showRemoveIcon) {
          <button
            type="button"
            nz-button
            nzType="text"
            nzSize="small"
            (click)="handleRemove(file, $event)"
            [attr.title]="locale.removeFile"
            class="ant-upload-list-item-card-actions-btn"
          >
            <nz-icon nzType="delete" />
          </button>
        }
      </ng-template>

      <ng-template #downloadIcon>
        @if (file.showDownload) {
          <button
            type="button"
            nz-button
            nzType="text"
            nzSize="small"
            (click)="handleDownload(file)"
            [attr.title]="locale.downloadFile"
            class="ant-upload-list-item-card-actions-btn"
          >
            <nz-icon nzType="download" />
          </button>
        }
      </ng-template>

      <ng-template #downloadOrDelete>
        @if (_listType !== 'picture-card') {
          <span class="ant-upload-list-item-card-actions" [class.picture]="_listType === 'picture'">
            <ng-template [ngTemplateOutlet]="downloadIcon"></ng-template>
            <ng-template [ngTemplateOutlet]="removeIcon"></ng-template>
          </span>
        }
      </ng-template>

      <ng-template #preview>
        @if (file.url) {
          <a
            target="_blank"
            rel="noopener noreferrer"
            class="ant-upload-list-item-name"
            [attr.title]="file.name"
            [href]="file.url"
            [attr.download]="file.linkProps && file.linkProps.download"
            (click)="handlePreview(file, $event)"
          >
            {{ file.name }}
          </a>
        } @else {
          <span class="ant-upload-list-item-name" [attr.title]="file.name" (click)="handlePreview(file, $event)">
            {{ file.name }}
          </span>
        }
        <ng-template [ngTemplateOutlet]="downloadOrDelete"></ng-template>
      </ng-template>

      <div class="ant-upload-list-item-info">
        <span class="ant-upload-span">
          <ng-template [ngTemplateOutlet]="icon"></ng-template>
          <ng-template [ngTemplateOutlet]="preview"></ng-template>
        </span>
      </div>
      @if (!file.isUploading) {
        @if (_listType === 'picture-card') {
          <span class="ant-upload-list-item-actions">
            @if (icons.showPreviewIcon) {
              <a
                [href]="file.url || file.thumbUrl"
                target="_blank"
                rel="noopener noreferrer"
                [attr.title]="locale.previewFile"
                [style]="!(file.url || file.thumbUrl) ? { opacity: 0.5, 'pointer-events': 'none' } : null"
                (click)="handlePreview(file, $event)"
              >
                <nz-icon nzType="eye" />
              </a>
            }
            @if (file.status === 'done') {
              <ng-template [ngTemplateOutlet]="downloadIcon"></ng-template>
            }
            <ng-template [ngTemplateOutlet]="removeIcon"></ng-template>
          </span>
        }
      } @else {
        <div class="ant-upload-list-item-progress">
          <nz-progress [nzPercent]="file.percent!" nzType="line" [nzShowInfo]="false" [nzStrokeWidth]="2"></nz-progress>
        </div>
      }
    </div>
  </div>
}
`
    }]
  }], null, {
    listType: [{
      type: Input,
      args: [{
        isSignal: true,
        alias: "listType",
        required: false
      }]
    }],
    locale: [{
      type: Input
    }],
    items: [{
      type: Input
    }],
    icons: [{
      type: Input
    }],
    onPreview: [{
      type: Input
    }],
    onRemove: [{
      type: Input
    }],
    onDownload: [{
      type: Input
    }],
    previewFile: [{
      type: Input
    }],
    previewIsImage: [{
      type: Input
    }],
    iconRender: [{
      type: Input
    }]
  });
})();
var CLASS_NAME = "ant-upload";
var NzUploadComponent = class _NzUploadComponent {
  static ngAcceptInputType_nzShowUploadList;
  cdr = inject(ChangeDetectorRef);
  i18n = inject(NzI18nService);
  destroyRef = inject(DestroyRef);
  document = inject(DOCUMENT);
  platform = inject(Platform);
  dir = inject(Directionality).valueSignal;
  uploadComp;
  listComp;
  locale;
  // #region fields
  nzType = "select";
  nzLimit = 0;
  nzSize = 0;
  nzFileType;
  nzAccept;
  nzAction;
  nzDirectory = false;
  nzOpenFileDialogOnClick = true;
  nzBeforeUpload;
  nzCustomRequest;
  nzData;
  nzFilter = [];
  nzFileList = [];
  nzDisabled = false;
  nzHeaders;
  nzListType = "text";
  nzMultiple = false;
  nzName = "file";
  _showUploadList = true;
  set nzShowUploadList(value) {
    this._showUploadList = typeof value === "boolean" ? toBoolean(value) : value;
  }
  get nzShowUploadList() {
    return this._showUploadList;
  }
  nzShowButton = true;
  nzWithCredentials = false;
  nzRemove;
  nzPreview;
  nzPreviewFile;
  nzPreviewIsImage;
  /**
   * @deprecated will be removed in v22.0.0
   * Use `nzBeforeUpload` instead.
   */
  nzTransformFile;
  nzDownload;
  nzIconRender = null;
  nzFileListRender = null;
  nzMaxCount = input(...ngDevMode ? [void 0, {
    debugName: "nzMaxCount"
  }] : []);
  nzChange = new EventEmitter();
  nzFileListChange = new EventEmitter();
  _btnOptions;
  zipOptions() {
    if (typeof this.nzShowUploadList === "boolean" && this.nzShowUploadList) {
      this.nzShowUploadList = {
        showPreviewIcon: true,
        showRemoveIcon: true,
        showDownloadIcon: true
      };
    }
    const filters = this.nzFilter.slice();
    if (this.nzMultiple && this.nzLimit > 0 && filters.findIndex((w) => w.name === "limit") === -1) {
      filters.push({
        name: "limit",
        fn: (fileList) => fileList.slice(-this.nzLimit)
      });
    }
    if (this.nzSize > 0 && filters.findIndex((w) => w.name === "size") === -1) {
      filters.push({
        name: "size",
        fn: (fileList) => fileList.filter((w) => w.size / 1024 <= this.nzSize)
      });
    }
    if (this.nzFileType && this.nzFileType.length > 0 && filters.findIndex((w) => w.name === "type") === -1) {
      const types = this.nzFileType.split(",");
      filters.push({
        name: "type",
        fn: (fileList) => fileList.filter((w) => ~types.indexOf(w.type))
      });
    }
    this._btnOptions = {
      disabled: this.nzDisabled,
      accept: this.nzAccept,
      action: this.nzAction,
      directory: this.nzDirectory,
      openFileDialogOnClick: this.nzOpenFileDialogOnClick,
      beforeUpload: this.nzBeforeUpload,
      customRequest: this.nzCustomRequest,
      data: this.nzData,
      headers: this.nzHeaders,
      name: this.nzName,
      multiple: this.nzMultiple,
      withCredentials: this.nzWithCredentials,
      filters,
      transformFile: this.nzTransformFile,
      onStart: this.onStart,
      onProgress: this.onProgress,
      onSuccess: this.onSuccess,
      onError: this.onError
    };
    return this;
  }
  // #endregion
  // #region upload
  fileToObject(file) {
    return {
      lastModified: file.lastModified,
      lastModifiedDate: file.lastModifiedDate,
      name: file.filename || file.name,
      size: file.size,
      type: file.type,
      uid: file.uid,
      response: file.response,
      error: file.error,
      percent: 0,
      originFileObj: file
    };
  }
  getFileItem(file, fileList) {
    return fileList.filter((item) => item.uid === file.uid)[0];
  }
  removeFileItem(file, fileList) {
    return fileList.filter((item) => item.uid !== file.uid);
  }
  onStart = (file) => {
    const maxCount = this.nzMaxCount();
    if (!this.nzFileList) {
      this.nzFileList = [];
    }
    const targetItem = this.fileToObject(file);
    targetItem.status = "uploading";
    if (maxCount === 1) {
      this.nzFileList = [targetItem];
    } else if (!maxCount || maxCount <= 0 || this.nzFileList.length < maxCount) {
      this.nzFileList = [...this.nzFileList, targetItem];
    }
    this.nzFileListChange.emit(this.nzFileList);
    this.nzChange.emit({
      file: targetItem,
      fileList: this.nzFileList,
      type: "start"
    });
    this.detectChangesList();
  };
  onProgress = (e, file) => {
    const fileList = this.nzFileList;
    const targetItem = this.getFileItem(file, fileList);
    if (!targetItem) {
      return;
    }
    targetItem.percent = e.percent;
    this.nzChange.emit({
      event: e,
      file: targetItem,
      fileList: this.nzFileList,
      type: "progress"
    });
    this.detectChangesList();
  };
  onSuccess = (res, file) => {
    const fileList = this.nzFileList;
    const targetItem = this.getFileItem(file, fileList);
    if (!targetItem) {
      return;
    }
    targetItem.status = "done";
    targetItem.response = res;
    this.nzChange.emit({
      file: targetItem,
      fileList,
      type: "success"
    });
    this.detectChangesList();
  };
  onError = (err, file) => {
    const fileList = this.nzFileList;
    const targetItem = this.getFileItem(file, fileList);
    if (!targetItem) {
      return;
    }
    targetItem.error = err;
    targetItem.status = "error";
    this.nzChange.emit({
      file: __spreadValues({}, targetItem),
      fileList,
      type: "error"
    });
    this.detectChangesList();
  };
  // #endregion
  // #region drag
  dragState;
  // skip safari bug
  fileDrop(e) {
    if (e.type === this.dragState) {
      return;
    }
    this.dragState = e.type;
    this.setClassMap();
  }
  // #endregion
  // #region list
  detectChangesList() {
    this.cdr.detectChanges();
    this.listComp?.detectChanges();
  }
  onRemove = (file) => {
    this.uploadComp.abort(file);
    file.status = "removed";
    const fnRes = typeof this.nzRemove === "function" ? this.nzRemove(file) : this.nzRemove == null ? true : this.nzRemove;
    (fnRes instanceof Observable ? fnRes : of(fnRes)).pipe(filter((res) => res)).subscribe(() => {
      this.nzFileList = this.removeFileItem(file, this.nzFileList);
      this.nzChange.emit({
        file,
        fileList: this.nzFileList,
        type: "removed"
      });
      this.nzFileListChange.emit(this.nzFileList);
      this.cdr.detectChanges();
    });
  };
  // #endregion
  // #region styles
  classList = signal([], ...ngDevMode ? [{
    debugName: "classList"
  }] : []);
  setClassMap() {
    let subCls = [];
    if (this.nzType === "drag") {
      if (this.nzFileList.some((file) => file.status === "uploading")) {
        subCls.push(this.generateClass("drag-uploading"));
      }
      if (this.dragState === "dragover") {
        subCls.push(this.generateClass("drag-hover"));
      }
    } else {
      subCls = [this.generateClass(`select-${this.nzListType}`)];
    }
    if (this.nzDisabled) {
      subCls.push(this.generateClass("disabled"));
    }
    this.classList.set([CLASS_NAME, this.generateClass(this.nzType), ...subCls]);
  }
  // #endregion
  ngOnInit() {
    this.i18n.localeChange.pipe(takeUntilDestroyed(this.destroyRef)).subscribe(() => {
      this.locale = this.i18n.getLocaleData("Upload");
      this.detectChangesList();
    });
  }
  ngAfterViewInit() {
    if (this.platform.FIREFOX) {
      fromEventOutsideAngular(this.document.body, "drop").pipe(takeUntilDestroyed(this.destroyRef)).subscribe((event) => {
        event.preventDefault();
        event.stopPropagation();
      });
    }
  }
  ngOnChanges() {
    this.zipOptions().setClassMap();
  }
  generateClass(suffix) {
    return generateClassName(CLASS_NAME, suffix);
  }
  static ɵfac = function NzUploadComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzUploadComponent)();
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _NzUploadComponent,
    selectors: [["nz-upload"]],
    viewQuery: function NzUploadComponent_Query(rf, ctx) {
      if (rf & 1) {
        ɵɵviewQuery(_c5, 5)(_c6, 5);
      }
      if (rf & 2) {
        let _t;
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.uploadComp = _t.first);
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.listComp = _t.first);
      }
    },
    hostVars: 2,
    hostBindings: function NzUploadComponent_HostBindings(rf, ctx) {
      if (rf & 2) {
        ɵɵclassProp("ant-upload-picture-card-wrapper", ctx.nzListType === "picture-card");
      }
    },
    inputs: {
      nzType: "nzType",
      nzLimit: [2, "nzLimit", "nzLimit", numberAttribute],
      nzSize: [2, "nzSize", "nzSize", numberAttribute],
      nzFileType: "nzFileType",
      nzAccept: "nzAccept",
      nzAction: "nzAction",
      nzDirectory: [2, "nzDirectory", "nzDirectory", booleanAttribute],
      nzOpenFileDialogOnClick: [2, "nzOpenFileDialogOnClick", "nzOpenFileDialogOnClick", booleanAttribute],
      nzBeforeUpload: "nzBeforeUpload",
      nzCustomRequest: "nzCustomRequest",
      nzData: "nzData",
      nzFilter: "nzFilter",
      nzFileList: "nzFileList",
      nzDisabled: [2, "nzDisabled", "nzDisabled", booleanAttribute],
      nzHeaders: "nzHeaders",
      nzListType: "nzListType",
      nzMultiple: [2, "nzMultiple", "nzMultiple", booleanAttribute],
      nzName: "nzName",
      nzShowUploadList: "nzShowUploadList",
      nzShowButton: [2, "nzShowButton", "nzShowButton", booleanAttribute],
      nzWithCredentials: [2, "nzWithCredentials", "nzWithCredentials", booleanAttribute],
      nzRemove: "nzRemove",
      nzPreview: "nzPreview",
      nzPreviewFile: "nzPreviewFile",
      nzPreviewIsImage: "nzPreviewIsImage",
      nzTransformFile: "nzTransformFile",
      nzDownload: "nzDownload",
      nzIconRender: "nzIconRender",
      nzFileListRender: "nzFileListRender",
      nzMaxCount: [1, "nzMaxCount"]
    },
    outputs: {
      nzChange: "nzChange",
      nzFileListChange: "nzFileListChange"
    },
    exportAs: ["nzUpload"],
    features: [ɵɵNgOnChangesFeature],
    ngContentSelectors: _c2,
    decls: 8,
    vars: 1,
    consts: [["list", ""], ["con", ""], ["btn", ""], ["listComp", ""], ["uploadComp", ""], [3, "display", "locale", "listType", "items", "icons", "iconRender", "previewFile", "previewIsImage", "onPreview", "onRemove", "onDownload"], [3, "locale", "listType", "items", "icons", "iconRender", "previewFile", "previewIsImage", "onPreview", "onRemove", "onDownload"], [4, "ngTemplateOutlet", "ngTemplateOutletContext"], ["nz-upload-btn", "", 3, "options"], [3, "ngTemplateOutlet"], [3, "drop", "dragover", "dragleave"], ["nz-upload-btn", "", 1, "ant-upload-btn", 3, "options"], [1, "ant-upload-drag-container"]],
    template: function NzUploadComponent_Template(rf, ctx) {
      if (rf & 1) {
        ɵɵprojectionDef();
        ɵɵtemplate(0, NzUploadComponent_ng_template_0_Template, 2, 2, "ng-template", null, 0, ɵɵtemplateRefExtractor)(2, NzUploadComponent_ng_template_2_Template, 1, 0, "ng-template", null, 1, ɵɵtemplateRefExtractor)(4, NzUploadComponent_ng_template_4_Template, 4, 8, "ng-template", null, 2, ɵɵtemplateRefExtractor);
        ɵɵconditionalCreate(6, NzUploadComponent_Conditional_6_Template, 6, 7)(7, NzUploadComponent_Conditional_7_Template, 2, 1);
      }
      if (rf & 2) {
        ɵɵadvance(6);
        ɵɵconditional(ctx.nzType === "drag" ? 6 : 7);
      }
    },
    dependencies: [NzUploadListComponent, NgTemplateOutlet, NzUploadBtnComponent],
    encapsulation: 2,
    changeDetection: 0
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzUploadComponent, [{
    type: Component,
    args: [{
      selector: "nz-upload",
      exportAs: "nzUpload",
      encapsulation: ViewEncapsulation.None,
      changeDetection: ChangeDetectionStrategy.OnPush,
      host: {
        "[class.ant-upload-picture-card-wrapper]": 'nzListType === "picture-card"'
      },
      imports: [NzUploadListComponent, NgTemplateOutlet, NzUploadBtnComponent],
      template: `<ng-template #list>
  @if (locale && !nzFileListRender) {
    <nz-upload-list
      #listComp
      [style.display]="nzShowUploadList ? '' : 'none'"
      [locale]="locale"
      [listType]="nzListType"
      [items]="nzFileList || []"
      [icons]="$any(nzShowUploadList)"
      [iconRender]="nzIconRender"
      [previewFile]="nzPreviewFile"
      [previewIsImage]="nzPreviewIsImage"
      [onPreview]="nzPreview"
      [onRemove]="onRemove"
      [onDownload]="nzDownload"
    />
  }
  @if (nzFileListRender) {
    <ng-container *ngTemplateOutlet="nzFileListRender; context: { $implicit: nzFileList }"></ng-container>
  }
</ng-template>
<ng-template #con><ng-content></ng-content></ng-template>
<ng-template #btn>
  <div [class]="classList()" [class.ant-upload-rtl]="dir() === 'rtl'" [style.display]="nzShowButton ? '' : 'none'">
    <div nz-upload-btn #uploadComp [options]="_btnOptions!">
      <ng-template [ngTemplateOutlet]="con"></ng-template>
    </div>
  </div>
</ng-template>
@if (nzType === 'drag') {
  <div
    [class]="classList()"
    [class.ant-upload-rtl]="dir() === 'rtl'"
    (drop)="fileDrop($event)"
    (dragover)="fileDrop($event)"
    (dragleave)="fileDrop($event)"
  >
    <div nz-upload-btn #uploadComp [options]="_btnOptions!" class="ant-upload-btn">
      <div class="ant-upload-drag-container">
        <ng-template [ngTemplateOutlet]="con"></ng-template>
      </div>
    </div>
  </div>
  <ng-template [ngTemplateOutlet]="list"></ng-template>
} @else {
  @if (nzListType === 'picture-card') {
    <ng-template [ngTemplateOutlet]="list"></ng-template>
    <ng-template [ngTemplateOutlet]="btn"></ng-template>
  } @else {
    <ng-template [ngTemplateOutlet]="btn"></ng-template>
    <ng-template [ngTemplateOutlet]="list"></ng-template>
  }
}
`
    }]
  }], null, {
    uploadComp: [{
      type: ViewChild,
      args: ["uploadComp", {
        static: false
      }]
    }],
    listComp: [{
      type: ViewChild,
      args: ["listComp", {
        static: false
      }]
    }],
    nzType: [{
      type: Input
    }],
    nzLimit: [{
      type: Input,
      args: [{
        transform: numberAttribute
      }]
    }],
    nzSize: [{
      type: Input,
      args: [{
        transform: numberAttribute
      }]
    }],
    nzFileType: [{
      type: Input
    }],
    nzAccept: [{
      type: Input
    }],
    nzAction: [{
      type: Input
    }],
    nzDirectory: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzOpenFileDialogOnClick: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzBeforeUpload: [{
      type: Input
    }],
    nzCustomRequest: [{
      type: Input
    }],
    nzData: [{
      type: Input
    }],
    nzFilter: [{
      type: Input
    }],
    nzFileList: [{
      type: Input
    }],
    nzDisabled: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzHeaders: [{
      type: Input
    }],
    nzListType: [{
      type: Input
    }],
    nzMultiple: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzName: [{
      type: Input
    }],
    nzShowUploadList: [{
      type: Input
    }],
    nzShowButton: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzWithCredentials: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzRemove: [{
      type: Input
    }],
    nzPreview: [{
      type: Input
    }],
    nzPreviewFile: [{
      type: Input
    }],
    nzPreviewIsImage: [{
      type: Input
    }],
    nzTransformFile: [{
      type: Input
    }],
    nzDownload: [{
      type: Input
    }],
    nzIconRender: [{
      type: Input
    }],
    nzFileListRender: [{
      type: Input
    }],
    nzMaxCount: [{
      type: Input,
      args: [{
        isSignal: true,
        alias: "nzMaxCount",
        required: false
      }]
    }],
    nzChange: [{
      type: Output
    }],
    nzFileListChange: [{
      type: Output
    }]
  });
})();
var NzUploadModule = class _NzUploadModule {
  static ɵfac = function NzUploadModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzUploadModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NzUploadModule,
    imports: [NzUploadComponent, NzUploadBtnComponent, NzUploadListComponent],
    exports: [NzUploadComponent]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [NzUploadComponent, NzUploadListComponent]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzUploadModule, [{
    type: NgModule,
    args: [{
      imports: [NzUploadComponent, NzUploadBtnComponent, NzUploadListComponent],
      exports: [NzUploadComponent]
    }]
  }], null, null);
})();
export {
  NzUploadBtnComponent,
  NzUploadComponent,
  NzUploadListComponent,
  NzUploadModule
};
//# sourceMappingURL=ng-zorro-antd_upload.js.map
