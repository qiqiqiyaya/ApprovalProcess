import {
  NzTooltipBaseDirective,
  NzTooltipComponent,
  isTooltipEmpty
} from "./chunk-CXY6SVCO.js";
import {
  CdkConnectedOverlay,
  NzConnectedOverlayDirective,
  NzOverlayModule,
  OverlayModule
} from "./chunk-HSN4NN2U.js";
import "./chunk-MZ5D2MGO.js";
import "./chunk-4OODPXLP.js";
import "./chunk-RJK3RDQK.js";
import "./chunk-B7XDWOSB.js";
import "./chunk-W6VE2EMK.js";
import {
  NzNoAnimationDirective
} from "./chunk-SPYBVWE7.js";
import {
  NzOutletModule,
  NzStringTemplateOutletDirective
} from "./chunk-CWBX6YEP.js";
import "./chunk-QYDDKLT3.js";
import {
  WithConfig
} from "./chunk-M622CJQC.js";
import "./chunk-PKOG7UK4.js";
import "./chunk-HP6B2NEN.js";
import "./chunk-AMAGFN52.js";
import "./chunk-OXRDR26M.js";
import {
  ChangeDetectionStrategy,
  Component,
  Directive,
  EventEmitter,
  Input,
  NgModule,
  Output,
  ViewEncapsulation,
  booleanAttribute,
  setClassMetadata,
  ɵɵInheritDefinitionFeature,
  ɵɵadvance,
  ɵɵanimateEnter,
  ɵɵanimateLeave,
  ɵɵclassMap,
  ɵɵclassProp,
  ɵɵconditional,
  ɵɵconditionalCreate,
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
  ɵɵgetInheritedFactory,
  ɵɵlistener,
  ɵɵnextContext,
  ɵɵproperty,
  ɵɵpureFunction1,
  ɵɵresetView,
  ɵɵrestoreView,
  ɵɵstyleMap,
  ɵɵtemplate,
  ɵɵtemplateRefExtractor,
  ɵɵtext,
  ɵɵtextInterpolate1
} from "./chunk-4XV3JIPT.js";
import "./chunk-XZWRYGZ6.js";
import {
  __esDecorate,
  __runInitializers
} from "./chunk-DP5J3HDO.js";
import {
  __spreadValues
} from "./chunk-KWSTWQNB.js";

// node_modules/ng-zorro-antd/fesm2022/ng-zorro-antd-popover.mjs
var _c0 = (a0) => ({
  $implicit: a0
});
function NzPopoverComponent_ng_template_0_Conditional_5_ng_container_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtext(1);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext(3);
    ɵɵadvance();
    ɵɵtextInterpolate1(" ", ctx_r2.nzTitle, " ");
  }
}
function NzPopoverComponent_ng_template_0_Conditional_5_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 6);
    ɵɵtemplate(1, NzPopoverComponent_ng_template_0_Conditional_5_ng_container_1_Template, 2, 1, "ng-container", 8);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵproperty("nzStringTemplateOutlet", ctx_r2.nzTitle)("nzStringTemplateOutletContext", ɵɵpureFunction1(2, _c0, ctx_r2.nzTitleContext));
  }
}
function NzPopoverComponent_ng_template_0_ng_container_7_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtext(1);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵtextInterpolate1(" ", ctx_r2.nzContent, " ");
  }
}
function NzPopoverComponent_ng_template_0_Template(rf, ctx) {
  if (rf & 1) {
    const _r2 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 2);
    ɵɵanimateLeave(function NzPopoverComponent_ng_template_0_Template_animateleave_cb() {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.zoomAnimationLeave());
    });
    ɵɵanimateEnter(function NzPopoverComponent_ng_template_0_Template_animateenter_cb() {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.zoomAnimationEnter());
    });
    ɵɵelement(1, "div", 3);
    ɵɵelementStart(2, "div", 4)(3, "div", 5)(4, "div");
    ɵɵconditionalCreate(5, NzPopoverComponent_ng_template_0_Conditional_5_Template, 2, 4, "div", 6);
    ɵɵelementStart(6, "div", 7);
    ɵɵtemplate(7, NzPopoverComponent_ng_template_0_ng_container_7_Template, 2, 1, "ng-container", 8);
    ɵɵelementEnd()()()()();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext();
    ɵɵstyleMap(ctx_r2.nzOverlayStyle);
    ɵɵclassMap(ctx_r2._classMap);
    ɵɵclassProp("ant-popover-rtl", ctx_r2.dir() === "rtl");
    ɵɵproperty("nzNoAnimation", !!(ctx_r2.noAnimation == null ? null : ctx_r2.noAnimation.nzNoAnimation == null ? null : ctx_r2.noAnimation.nzNoAnimation()));
    ɵɵadvance(5);
    ɵɵconditional(ctx_r2.nzTitle ? 5 : -1);
    ɵɵadvance(2);
    ɵɵproperty("nzStringTemplateOutlet", ctx_r2.nzContent)("nzStringTemplateOutletContext", ɵɵpureFunction1(10, _c0, ctx_r2.nzContentContext));
  }
}
var NZ_CONFIG_MODULE_NAME = "popover";
var NzPopoverDirective = (() => {
  let _classSuper = NzTooltipBaseDirective;
  let _nzPopoverBackdrop_decorators;
  let _nzPopoverBackdrop_initializers = [];
  let _nzPopoverBackdrop_extraInitializers = [];
  return class NzPopoverDirective2 extends _classSuper {
    static {
      const _metadata = typeof Symbol === "function" && Symbol.metadata ? Object.create(_classSuper[Symbol.metadata] ?? null) : void 0;
      _nzPopoverBackdrop_decorators = [WithConfig()];
      __esDecorate(null, null, _nzPopoverBackdrop_decorators, {
        kind: "field",
        name: "nzPopoverBackdrop",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzPopoverBackdrop" in obj,
          get: (obj) => obj.nzPopoverBackdrop,
          set: (obj, value) => {
            obj.nzPopoverBackdrop = value;
          }
        },
        metadata: _metadata
      }, _nzPopoverBackdrop_initializers, _nzPopoverBackdrop_extraInitializers);
      if (_metadata) Object.defineProperty(this, Symbol.metadata, {
        enumerable: true,
        configurable: true,
        writable: true,
        value: _metadata
      });
    }
    _nzModuleName = NZ_CONFIG_MODULE_NAME;
    /* eslint-disable @angular-eslint/no-input-rename, @angular-eslint/no-output-rename */
    arrowPointAtCenter;
    title;
    titleContext = null;
    content;
    contentContext = null;
    directiveTitle;
    trigger = "hover";
    placement = "top";
    origin;
    visible;
    mouseEnterDelay;
    mouseLeaveDelay;
    overlayClassName;
    overlayStyle;
    overlayClickable;
    directiveContent = null;
    nzPopoverBackdrop = __runInitializers(this, _nzPopoverBackdrop_initializers, false);
    visibleChange = (__runInitializers(this, _nzPopoverBackdrop_extraInitializers), new EventEmitter());
    getProxyPropertyMap() {
      return __spreadValues({
        nzPopoverBackdrop: ["nzBackdrop", () => this.nzPopoverBackdrop],
        titleContext: ["nzTitleContext", () => this.titleContext],
        contentContext: ["nzContentContext", () => this.contentContext]
      }, super.getProxyPropertyMap());
    }
    constructor() {
      super(NzPopoverComponent);
    }
    static ɵfac = function NzPopoverDirective_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || NzPopoverDirective2)();
    };
    static ɵdir = ɵɵdefineDirective({
      type: NzPopoverDirective2,
      selectors: [["", "nz-popover", ""]],
      hostVars: 2,
      hostBindings: function NzPopoverDirective_HostBindings(rf, ctx) {
        if (rf & 2) {
          ɵɵclassProp("ant-popover-open", ctx.visible);
        }
      },
      inputs: {
        arrowPointAtCenter: [2, "nzPopoverArrowPointAtCenter", "arrowPointAtCenter", booleanAttribute],
        title: [0, "nzPopoverTitle", "title"],
        titleContext: [0, "nzPopoverTitleContext", "titleContext"],
        content: [0, "nzPopoverContent", "content"],
        contentContext: [0, "nzPopoverContentContext", "contentContext"],
        directiveTitle: [0, "nz-popover", "directiveTitle"],
        trigger: [0, "nzPopoverTrigger", "trigger"],
        placement: [0, "nzPopoverPlacement", "placement"],
        origin: [0, "nzPopoverOrigin", "origin"],
        visible: [0, "nzPopoverVisible", "visible"],
        mouseEnterDelay: [0, "nzPopoverMouseEnterDelay", "mouseEnterDelay"],
        mouseLeaveDelay: [0, "nzPopoverMouseLeaveDelay", "mouseLeaveDelay"],
        overlayClassName: [0, "nzPopoverOverlayClassName", "overlayClassName"],
        overlayStyle: [0, "nzPopoverOverlayStyle", "overlayStyle"],
        overlayClickable: [0, "nzPopoverOverlayClickable", "overlayClickable"],
        nzPopoverBackdrop: "nzPopoverBackdrop"
      },
      outputs: {
        visibleChange: "nzPopoverVisibleChange"
      },
      exportAs: ["nzPopover"],
      features: [ɵɵInheritDefinitionFeature]
    });
  };
})();
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzPopoverDirective, [{
    type: Directive,
    args: [{
      selector: "[nz-popover]",
      exportAs: "nzPopover",
      host: {
        "[class.ant-popover-open]": "visible"
      }
    }]
  }], () => [], {
    arrowPointAtCenter: [{
      type: Input,
      args: [{
        alias: "nzPopoverArrowPointAtCenter",
        transform: booleanAttribute
      }]
    }],
    title: [{
      type: Input,
      args: ["nzPopoverTitle"]
    }],
    titleContext: [{
      type: Input,
      args: ["nzPopoverTitleContext"]
    }],
    content: [{
      type: Input,
      args: ["nzPopoverContent"]
    }],
    contentContext: [{
      type: Input,
      args: ["nzPopoverContentContext"]
    }],
    directiveTitle: [{
      type: Input,
      args: ["nz-popover"]
    }],
    trigger: [{
      type: Input,
      args: ["nzPopoverTrigger"]
    }],
    placement: [{
      type: Input,
      args: ["nzPopoverPlacement"]
    }],
    origin: [{
      type: Input,
      args: ["nzPopoverOrigin"]
    }],
    visible: [{
      type: Input,
      args: ["nzPopoverVisible"]
    }],
    mouseEnterDelay: [{
      type: Input,
      args: ["nzPopoverMouseEnterDelay"]
    }],
    mouseLeaveDelay: [{
      type: Input,
      args: ["nzPopoverMouseLeaveDelay"]
    }],
    overlayClassName: [{
      type: Input,
      args: ["nzPopoverOverlayClassName"]
    }],
    overlayStyle: [{
      type: Input,
      args: ["nzPopoverOverlayStyle"]
    }],
    overlayClickable: [{
      type: Input,
      args: ["nzPopoverOverlayClickable"]
    }],
    nzPopoverBackdrop: [{
      type: Input
    }],
    visibleChange: [{
      type: Output,
      args: ["nzPopoverVisibleChange"]
    }]
  });
})();
var NzPopoverComponent = class _NzPopoverComponent extends NzTooltipComponent {
  _animationPrefix = "ant-zoom-big";
  _prefix = "ant-popover";
  nzContentContext = null;
  get hasBackdrop() {
    return this.nzTrigger === "click" ? this.nzBackdrop : false;
  }
  isEmpty() {
    return isTooltipEmpty(this.nzTitle) && isTooltipEmpty(this.nzContent);
  }
  static ɵfac = /* @__PURE__ */ (() => {
    let ɵNzPopoverComponent_BaseFactory;
    return function NzPopoverComponent_Factory(__ngFactoryType__) {
      return (ɵNzPopoverComponent_BaseFactory || (ɵNzPopoverComponent_BaseFactory = ɵɵgetInheritedFactory(_NzPopoverComponent)))(__ngFactoryType__ || _NzPopoverComponent);
    };
  })();
  static ɵcmp = ɵɵdefineComponent({
    type: _NzPopoverComponent,
    selectors: [["nz-popover"]],
    exportAs: ["nzPopoverComponent"],
    features: [ɵɵInheritDefinitionFeature],
    decls: 2,
    vars: 6,
    consts: [["overlay", "cdkConnectedOverlay"], ["cdkConnectedOverlay", "", "nzConnectedOverlay", "", 3, "overlayOutsideClick", "detach", "positionChange", "cdkConnectedOverlayHasBackdrop", "cdkConnectedOverlayOrigin", "cdkConnectedOverlayPositions", "cdkConnectedOverlayOpen", "cdkConnectedOverlayPush", "nzArrowPointAtCenter"], [1, "ant-popover", 3, "nzNoAnimation"], [1, "ant-popover-arrow"], [1, "ant-popover-content"], ["role", "tooltip", 1, "ant-popover-inner"], [1, "ant-popover-title"], [1, "ant-popover-inner-content"], [4, "nzStringTemplateOutlet", "nzStringTemplateOutletContext"]],
    template: function NzPopoverComponent_Template(rf, ctx) {
      if (rf & 1) {
        const _r1 = ɵɵgetCurrentView();
        ɵɵtemplate(0, NzPopoverComponent_ng_template_0_Template, 8, 12, "ng-template", 1, 0, ɵɵtemplateRefExtractor);
        ɵɵlistener("overlayOutsideClick", function NzPopoverComponent_Template_ng_template_overlayOutsideClick_0_listener($event) {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.onClickOutside($event));
        })("detach", function NzPopoverComponent_Template_ng_template_detach_0_listener() {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.hide());
        })("positionChange", function NzPopoverComponent_Template_ng_template_positionChange_0_listener($event) {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.onPositionChange($event));
        });
      }
      if (rf & 2) {
        ɵɵproperty("cdkConnectedOverlayHasBackdrop", ctx.hasBackdrop)("cdkConnectedOverlayOrigin", ctx.origin)("cdkConnectedOverlayPositions", ctx._positions)("cdkConnectedOverlayOpen", ctx._visible)("cdkConnectedOverlayPush", ctx.cdkConnectedOverlayPush)("nzArrowPointAtCenter", ctx.nzArrowPointAtCenter);
      }
    },
    dependencies: [OverlayModule, CdkConnectedOverlay, NzOverlayModule, NzConnectedOverlayDirective, NzNoAnimationDirective, NzOutletModule, NzStringTemplateOutletDirective],
    encapsulation: 2,
    changeDetection: 0
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzPopoverComponent, [{
    type: Component,
    args: [{
      selector: "nz-popover",
      exportAs: "nzPopoverComponent",
      changeDetection: ChangeDetectionStrategy.OnPush,
      encapsulation: ViewEncapsulation.None,
      template: `
    <ng-template
      #overlay="cdkConnectedOverlay"
      cdkConnectedOverlay
      nzConnectedOverlay
      [cdkConnectedOverlayHasBackdrop]="hasBackdrop"
      [cdkConnectedOverlayOrigin]="origin"
      [cdkConnectedOverlayPositions]="_positions"
      [cdkConnectedOverlayOpen]="_visible"
      [cdkConnectedOverlayPush]="cdkConnectedOverlayPush"
      [nzArrowPointAtCenter]="nzArrowPointAtCenter"
      (overlayOutsideClick)="onClickOutside($event)"
      (detach)="hide()"
      (positionChange)="onPositionChange($event)"
    >
      <div
        class="ant-popover"
        [class.ant-popover-rtl]="dir() === 'rtl'"
        [class]="_classMap"
        [style]="nzOverlayStyle"
        [nzNoAnimation]="!!noAnimation?.nzNoAnimation?.()"
        [animate.enter]="zoomAnimationEnter()"
        [animate.leave]="zoomAnimationLeave()"
      >
        <div class="ant-popover-arrow"></div>
        <div class="ant-popover-content">
          <div class="ant-popover-inner" role="tooltip">
            <div>
              @if (nzTitle) {
                <div class="ant-popover-title">
                  <ng-container *nzStringTemplateOutlet="nzTitle; context: { $implicit: nzTitleContext }">
                    {{ nzTitle }}
                  </ng-container>
                </div>
              }
              <div class="ant-popover-inner-content">
                <ng-container *nzStringTemplateOutlet="nzContent; context: { $implicit: nzContentContext }">
                  {{ nzContent }}
                </ng-container>
              </div>
            </div>
          </div>
        </div>
      </div>
    </ng-template>
  `,
      imports: [OverlayModule, NzOverlayModule, NzNoAnimationDirective, NzOutletModule]
    }]
  }], null, null);
})();
var NzPopoverModule = class _NzPopoverModule {
  static ɵfac = function NzPopoverModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzPopoverModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NzPopoverModule,
    imports: [NzPopoverDirective, NzPopoverComponent],
    exports: [NzPopoverDirective, NzPopoverComponent]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [NzPopoverComponent]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzPopoverModule, [{
    type: NgModule,
    args: [{
      imports: [NzPopoverDirective, NzPopoverComponent],
      exports: [NzPopoverDirective, NzPopoverComponent]
    }]
  }], null, null);
})();
export {
  NzPopoverComponent,
  NzPopoverDirective,
  NzPopoverModule
};
//# sourceMappingURL=ng-zorro-antd_popover.js.map
