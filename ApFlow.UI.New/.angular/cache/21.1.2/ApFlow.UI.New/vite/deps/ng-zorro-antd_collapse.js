import {
  NzAnimationCollapseDirective
} from "./chunk-SPYBVWE7.js";
import "./chunk-QYDDKLT3.js";
import {
  NzOutletModule,
  NzStringTemplateOutletDirective
} from "./chunk-CWBX6YEP.js";
import {
  Directionality
} from "./chunk-AMAGFN52.js";
import {
  NzIconDirective,
  NzIconModule
} from "./chunk-HQM66OPU.js";
import "./chunk-72DKPDI6.js";
import "./chunk-W6VE2EMK.js";
import {
  WithConfig,
  onConfigChangeEventForComponent
} from "./chunk-M622CJQC.js";
import {
  takeUntilDestroyed
} from "./chunk-PKOG7UK4.js";
import {
  fromEventOutsideAngular
} from "./chunk-HP6B2NEN.js";
import "./chunk-BQ76GOFF.js";
import "./chunk-N2XNJ4XE.js";
import "./chunk-OXRDR26M.js";
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  DestroyRef,
  ElementRef,
  Input,
  NgModule,
  NgZone,
  Output,
  ViewChild,
  ViewEncapsulation,
  booleanAttribute,
  inject,
  input,
  linkedSignal,
  output,
  setClassMetadata,
  viewChild,
  ɵɵadvance,
  ɵɵattribute,
  ɵɵclassProp,
  ɵɵconditional,
  ɵɵconditionalCreate,
  ɵɵdefineComponent,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵelement,
  ɵɵelementContainerEnd,
  ɵɵelementContainerStart,
  ɵɵelementEnd,
  ɵɵelementStart,
  ɵɵnextContext,
  ɵɵprojection,
  ɵɵprojectionDef,
  ɵɵproperty,
  ɵɵqueryAdvance,
  ɵɵtemplate,
  ɵɵtext,
  ɵɵtextInterpolate,
  ɵɵviewQuerySignal
} from "./chunk-4XV3JIPT.js";
import {
  filter
} from "./chunk-XZWRYGZ6.js";
import {
  __esDecorate,
  __runInitializers
} from "./chunk-DP5J3HDO.js";
import {
  __spreadProps,
  __spreadValues
} from "./chunk-KWSTWQNB.js";

// node_modules/ng-zorro-antd/fesm2022/ng-zorro-antd-collapse.mjs
var _c0 = ["*"];
var _c1 = ["collapseHeader"];
var _c2 = ["collapseIcon"];
function NzCollapsePanelComponent_Conditional_2_ng_container_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵelement(1, "nz-icon", 9);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const expandedIcon_r1 = ctx.$implicit;
    const ctx_r1 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵproperty("nzType", expandedIcon_r1 || "right")("nzRotate", ctx_r1.active() ? 90 : 0);
  }
}
function NzCollapsePanelComponent_Conditional_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 3, 1);
    ɵɵtemplate(2, NzCollapsePanelComponent_Conditional_2_ng_container_2_Template, 2, 2, "ng-container", 5);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵadvance(2);
    ɵɵproperty("nzStringTemplateOutlet", ctx_r1.nzExpandedIcon);
  }
}
function NzCollapsePanelComponent_ng_container_4_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtext(1);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵtextInterpolate(ctx_r1.nzHeader);
  }
}
function NzCollapsePanelComponent_Conditional_5_ng_container_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtext(1);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵtextInterpolate(ctx_r1.nzExtra);
  }
}
function NzCollapsePanelComponent_Conditional_5_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 6);
    ɵɵtemplate(1, NzCollapsePanelComponent_Conditional_5_ng_container_1_Template, 2, 1, "ng-container", 5);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵproperty("nzStringTemplateOutlet", ctx_r1.nzExtra);
  }
}
var NZ_CONFIG_MODULE_NAME$1 = "collapse";
var NzCollapseComponent = (() => {
  let _nzAccordion_decorators;
  let _nzAccordion_initializers = [];
  let _nzAccordion_extraInitializers = [];
  let _nzBordered_decorators;
  let _nzBordered_initializers = [];
  let _nzBordered_extraInitializers = [];
  let _nzGhost_decorators;
  let _nzGhost_initializers = [];
  let _nzGhost_extraInitializers = [];
  return class NzCollapseComponent2 {
    static {
      const _metadata = typeof Symbol === "function" && Symbol.metadata ? /* @__PURE__ */ Object.create(null) : void 0;
      _nzAccordion_decorators = [WithConfig()];
      _nzBordered_decorators = [WithConfig()];
      _nzGhost_decorators = [WithConfig()];
      __esDecorate(null, null, _nzAccordion_decorators, {
        kind: "field",
        name: "nzAccordion",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzAccordion" in obj,
          get: (obj) => obj.nzAccordion,
          set: (obj, value) => {
            obj.nzAccordion = value;
          }
        },
        metadata: _metadata
      }, _nzAccordion_initializers, _nzAccordion_extraInitializers);
      __esDecorate(null, null, _nzBordered_decorators, {
        kind: "field",
        name: "nzBordered",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzBordered" in obj,
          get: (obj) => obj.nzBordered,
          set: (obj, value) => {
            obj.nzBordered = value;
          }
        },
        metadata: _metadata
      }, _nzBordered_initializers, _nzBordered_extraInitializers);
      __esDecorate(null, null, _nzGhost_decorators, {
        kind: "field",
        name: "nzGhost",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzGhost" in obj,
          get: (obj) => obj.nzGhost,
          set: (obj, value) => {
            obj.nzGhost = value;
          }
        },
        metadata: _metadata
      }, _nzGhost_initializers, _nzGhost_extraInitializers);
      if (_metadata) Object.defineProperty(this, Symbol.metadata, {
        enumerable: true,
        configurable: true,
        writable: true,
        value: _metadata
      });
    }
    cdr = inject(ChangeDetectorRef);
    dir = inject(Directionality).valueSignal;
    _nzModuleName = NZ_CONFIG_MODULE_NAME$1;
    nzAccordion = __runInitializers(this, _nzAccordion_initializers, false);
    nzBordered = (__runInitializers(this, _nzAccordion_extraInitializers), __runInitializers(this, _nzBordered_initializers, true));
    nzGhost = (__runInitializers(this, _nzBordered_extraInitializers), __runInitializers(this, _nzGhost_initializers, false));
    nzExpandIconPosition = (__runInitializers(this, _nzGhost_extraInitializers), "start");
    nzSize = "middle";
    listOfNzCollapsePanelComponent = [];
    constructor() {
      onConfigChangeEventForComponent(NZ_CONFIG_MODULE_NAME$1, () => this.cdr.markForCheck());
    }
    addPanel(value) {
      this.listOfNzCollapsePanelComponent.push(value);
    }
    removePanel(value) {
      this.listOfNzCollapsePanelComponent.splice(this.listOfNzCollapsePanelComponent.indexOf(value), 1);
    }
    click(collapse) {
      const active = collapse.active();
      if (this.nzAccordion && !active) {
        this.listOfNzCollapsePanelComponent.filter((item) => item !== collapse && item.active()).forEach((item) => item.activate(false));
      }
      collapse.activate(!active);
    }
    static ɵfac = function NzCollapseComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || NzCollapseComponent2)();
    };
    static ɵcmp = ɵɵdefineComponent({
      type: NzCollapseComponent2,
      selectors: [["nz-collapse"]],
      hostAttrs: [1, "ant-collapse"],
      hostVars: 14,
      hostBindings: function NzCollapseComponent_HostBindings(rf, ctx) {
        if (rf & 2) {
          ɵɵclassProp("ant-collapse-icon-position-start", ctx.nzExpandIconPosition === "start")("ant-collapse-icon-position-end", ctx.nzExpandIconPosition === "end")("ant-collapse-ghost", ctx.nzGhost)("ant-collapse-borderless", !ctx.nzBordered)("ant-collapse-rtl", ctx.dir() === "rtl")("ant-collapse-small", ctx.nzSize === "small")("ant-collapse-large", ctx.nzSize === "large");
        }
      },
      inputs: {
        nzAccordion: [2, "nzAccordion", "nzAccordion", booleanAttribute],
        nzBordered: [2, "nzBordered", "nzBordered", booleanAttribute],
        nzGhost: [2, "nzGhost", "nzGhost", booleanAttribute],
        nzExpandIconPosition: "nzExpandIconPosition",
        nzSize: "nzSize"
      },
      exportAs: ["nzCollapse"],
      ngContentSelectors: _c0,
      decls: 1,
      vars: 0,
      template: function NzCollapseComponent_Template(rf, ctx) {
        if (rf & 1) {
          ɵɵprojectionDef();
          ɵɵprojection(0);
        }
      },
      encapsulation: 2,
      changeDetection: 0
    });
  };
})();
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzCollapseComponent, [{
    type: Component,
    args: [{
      selector: "nz-collapse",
      exportAs: "nzCollapse",
      changeDetection: ChangeDetectionStrategy.OnPush,
      encapsulation: ViewEncapsulation.None,
      template: `<ng-content></ng-content>`,
      host: {
        class: "ant-collapse",
        "[class.ant-collapse-icon-position-start]": `nzExpandIconPosition === 'start'`,
        "[class.ant-collapse-icon-position-end]": `nzExpandIconPosition === 'end'`,
        "[class.ant-collapse-ghost]": `nzGhost`,
        "[class.ant-collapse-borderless]": "!nzBordered",
        "[class.ant-collapse-rtl]": `dir() === 'rtl'`,
        "[class.ant-collapse-small]": `nzSize === 'small'`,
        "[class.ant-collapse-large]": `nzSize === 'large'`
      }
    }]
  }], () => [], {
    nzAccordion: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzBordered: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzGhost: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzExpandIconPosition: [{
      type: Input
    }],
    nzSize: [{
      type: Input
    }]
  });
})();
var NZ_CONFIG_MODULE_NAME = "collapsePanel";
var NzCollapsePanelComponent = (() => {
  let _nzShowArrow_decorators;
  let _nzShowArrow_initializers = [];
  let _nzShowArrow_extraInitializers = [];
  return class NzCollapsePanelComponent2 {
    static {
      const _metadata = typeof Symbol === "function" && Symbol.metadata ? /* @__PURE__ */ Object.create(null) : void 0;
      _nzShowArrow_decorators = [WithConfig()];
      __esDecorate(null, null, _nzShowArrow_decorators, {
        kind: "field",
        name: "nzShowArrow",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzShowArrow" in obj,
          get: (obj) => obj.nzShowArrow,
          set: (obj, value) => {
            obj.nzShowArrow = value;
          }
        },
        metadata: _metadata
      }, _nzShowArrow_initializers, _nzShowArrow_extraInitializers);
      if (_metadata) Object.defineProperty(this, Symbol.metadata, {
        enumerable: true,
        configurable: true,
        writable: true,
        value: _metadata
      });
    }
    ngZone = inject(NgZone);
    cdr = inject(ChangeDetectorRef);
    destroyRef = inject(DestroyRef);
    nzCollapseComponent = inject(NzCollapseComponent, {
      host: true
    });
    _nzModuleName = NZ_CONFIG_MODULE_NAME;
    nzActive = input(false, ...ngDevMode ? [{
      debugName: "nzActive",
      transform: booleanAttribute
    }] : [{
      transform: booleanAttribute
    }]);
    /**
     * @deprecated Will be removed in v22, please use `nzCollapsible` with the value `'disabled'` instead.
     */
    nzDisabled = false;
    nzShowArrow = __runInitializers(this, _nzShowArrow_initializers, true);
    nzExtra = __runInitializers(this, _nzShowArrow_extraInitializers);
    nzHeader;
    nzExpandedIcon;
    nzCollapsible;
    nzActiveChange = output();
    /**
     * @description Actual active state of the panel.
     */
    active = linkedSignal(() => this.nzActive(), ...ngDevMode ? [{
      debugName: "active"
    }] : []);
    collapseHeader = viewChild.required("collapseHeader", {
      read: ElementRef
    });
    collapseIcon = viewChild("collapseIcon", ...ngDevMode ? [{
      debugName: "collapseIcon",
      read: ElementRef
    }] : [{
      read: ElementRef
    }]);
    constructor() {
      onConfigChangeEventForComponent(NZ_CONFIG_MODULE_NAME, () => this.cdr.markForCheck());
      this.nzCollapseComponent.addPanel(this);
      this.destroyRef.onDestroy(() => {
        this.nzCollapseComponent.removePanel(this);
      });
    }
    ngAfterViewInit() {
      const icon = this.collapseIcon();
      const header = this.collapseHeader();
      const element = this.nzShowArrow && this.nzCollapsible === "icon" && icon ? icon.nativeElement : header.nativeElement;
      fromEventOutsideAngular(element, "click").pipe(filter(() => !this.nzDisabled && this.nzCollapsible !== "disabled"), takeUntilDestroyed(this.destroyRef)).subscribe(() => {
        this.ngZone.run(() => {
          this.nzCollapseComponent.click(this);
        });
      });
    }
    activate(active) {
      this.active.set(active);
      this.nzActiveChange.emit(active);
    }
    static ɵfac = function NzCollapsePanelComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || NzCollapsePanelComponent2)();
    };
    static ɵcmp = ɵɵdefineComponent({
      type: NzCollapsePanelComponent2,
      selectors: [["nz-collapse-panel"]],
      viewQuery: function NzCollapsePanelComponent_Query(rf, ctx) {
        if (rf & 1) {
          ɵɵviewQuerySignal(ctx.collapseHeader, _c1, 5, ElementRef)(ctx.collapseIcon, _c2, 5, ElementRef);
        }
        if (rf & 2) {
          ɵɵqueryAdvance(2);
        }
      },
      hostAttrs: [1, "ant-collapse-item"],
      hostVars: 6,
      hostBindings: function NzCollapsePanelComponent_HostBindings(rf, ctx) {
        if (rf & 2) {
          ɵɵclassProp("ant-collapse-no-arrow", !ctx.nzShowArrow)("ant-collapse-item-active", ctx.active())("ant-collapse-item-disabled", ctx.nzDisabled || ctx.nzCollapsible === "disabled");
        }
      },
      inputs: {
        nzActive: [1, "nzActive"],
        nzDisabled: [2, "nzDisabled", "nzDisabled", booleanAttribute],
        nzShowArrow: [2, "nzShowArrow", "nzShowArrow", booleanAttribute],
        nzExtra: "nzExtra",
        nzHeader: "nzHeader",
        nzExpandedIcon: "nzExpandedIcon",
        nzCollapsible: "nzCollapsible"
      },
      outputs: {
        nzActiveChange: "nzActiveChange"
      },
      exportAs: ["nzCollapsePanel"],
      ngContentSelectors: _c0,
      decls: 9,
      vars: 11,
      consts: [["collapseHeader", ""], ["collapseIcon", ""], ["role", "button", 1, "ant-collapse-header"], ["role", "button", 1, "ant-collapse-expand-icon"], [1, "ant-collapse-header-text"], [4, "nzStringTemplateOutlet"], [1, "ant-collapse-extra"], ["animation-collapse", "", "leavedClassName", "ant-collapse-content-hidden", 1, "ant-collapse-content", 3, "open"], [1, "ant-collapse-content-box"], [1, "ant-collapse-arrow", 3, "nzType", "nzRotate"]],
      template: function NzCollapsePanelComponent_Template(rf, ctx) {
        if (rf & 1) {
          ɵɵprojectionDef();
          ɵɵelementStart(0, "div", 2, 0);
          ɵɵconditionalCreate(2, NzCollapsePanelComponent_Conditional_2_Template, 3, 1, "div", 3);
          ɵɵelementStart(3, "span", 4);
          ɵɵtemplate(4, NzCollapsePanelComponent_ng_container_4_Template, 2, 1, "ng-container", 5);
          ɵɵelementEnd();
          ɵɵconditionalCreate(5, NzCollapsePanelComponent_Conditional_5_Template, 2, 1, "div", 6);
          ɵɵelementEnd();
          ɵɵelementStart(6, "div", 7)(7, "div", 8);
          ɵɵprojection(8);
          ɵɵelementEnd()();
        }
        if (rf & 2) {
          ɵɵclassProp("ant-collapse-icon-collapsible-only", ctx.nzCollapsible === "icon")("ant-collapse-header-collapsible-only", ctx.nzCollapsible === "header");
          ɵɵattribute("aria-expanded", ctx.active());
          ɵɵadvance(2);
          ɵɵconditional(ctx.nzShowArrow ? 2 : -1);
          ɵɵadvance(2);
          ɵɵproperty("nzStringTemplateOutlet", ctx.nzHeader);
          ɵɵadvance();
          ɵɵconditional(ctx.nzExtra ? 5 : -1);
          ɵɵadvance();
          ɵɵclassProp("ant-collapse-content-active", ctx.active());
          ɵɵproperty("open", ctx.active());
        }
      },
      dependencies: [NzOutletModule, NzStringTemplateOutletDirective, NzIconModule, NzIconDirective, NzAnimationCollapseDirective],
      encapsulation: 2,
      changeDetection: 0
    });
  };
})();
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzCollapsePanelComponent, [{
    type: Component,
    args: [{
      selector: "nz-collapse-panel",
      exportAs: "nzCollapsePanel",
      changeDetection: ChangeDetectionStrategy.OnPush,
      encapsulation: ViewEncapsulation.None,
      template: `
    <div
      #collapseHeader
      role="button"
      [attr.aria-expanded]="active()"
      class="ant-collapse-header"
      [class.ant-collapse-icon-collapsible-only]="nzCollapsible === 'icon'"
      [class.ant-collapse-header-collapsible-only]="nzCollapsible === 'header'"
    >
      @if (nzShowArrow) {
        <div role="button" #collapseIcon class="ant-collapse-expand-icon">
          <ng-container *nzStringTemplateOutlet="nzExpandedIcon; let expandedIcon">
            <nz-icon [nzType]="expandedIcon || 'right'" class="ant-collapse-arrow" [nzRotate]="active() ? 90 : 0" />
          </ng-container>
        </div>
      }
      <span class="ant-collapse-header-text">
        <ng-container *nzStringTemplateOutlet="nzHeader">{{ nzHeader }}</ng-container>
      </span>
      @if (nzExtra) {
        <div class="ant-collapse-extra">
          <ng-container *nzStringTemplateOutlet="nzExtra">{{ nzExtra }}</ng-container>
        </div>
      }
    </div>
    <div
      class="ant-collapse-content"
      [class.ant-collapse-content-active]="active()"
      animation-collapse
      [open]="active()"
      leavedClassName="ant-collapse-content-hidden"
    >
      <div class="ant-collapse-content-box">
        <ng-content />
      </div>
    </div>
  `,
      host: {
        class: "ant-collapse-item",
        "[class.ant-collapse-no-arrow]": "!nzShowArrow",
        "[class.ant-collapse-item-active]": "active()",
        "[class.ant-collapse-item-disabled]": 'nzDisabled || nzCollapsible === "disabled"'
      },
      imports: [NzOutletModule, NzIconModule, NzAnimationCollapseDirective]
    }]
  }], () => [], {
    nzActive: [{
      type: Input,
      args: [{
        isSignal: true,
        alias: "nzActive",
        required: false
      }]
    }],
    nzDisabled: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzShowArrow: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzExtra: [{
      type: Input
    }],
    nzHeader: [{
      type: Input
    }],
    nzExpandedIcon: [{
      type: Input
    }],
    nzCollapsible: [{
      type: Input
    }],
    nzActiveChange: [{
      type: Output,
      args: ["nzActiveChange"]
    }],
    collapseHeader: [{
      type: ViewChild,
      args: ["collapseHeader", __spreadProps(__spreadValues({}, {
        read: ElementRef
      }), {
        isSignal: true
      })]
    }],
    collapseIcon: [{
      type: ViewChild,
      args: ["collapseIcon", __spreadProps(__spreadValues({}, {
        read: ElementRef
      }), {
        isSignal: true
      })]
    }]
  });
})();
var NzCollapseModule = class _NzCollapseModule {
  static ɵfac = function NzCollapseModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzCollapseModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NzCollapseModule,
    imports: [NzCollapsePanelComponent, NzCollapseComponent],
    exports: [NzCollapsePanelComponent, NzCollapseComponent]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [NzCollapsePanelComponent]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzCollapseModule, [{
    type: NgModule,
    args: [{
      imports: [NzCollapsePanelComponent, NzCollapseComponent],
      exports: [NzCollapsePanelComponent, NzCollapseComponent]
    }]
  }], null, null);
})();
export {
  NzCollapseComponent,
  NzCollapseModule,
  NzCollapsePanelComponent
};
//# sourceMappingURL=ng-zorro-antd_collapse.js.map
