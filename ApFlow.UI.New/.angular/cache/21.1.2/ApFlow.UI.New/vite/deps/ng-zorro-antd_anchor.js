import {
  NzAffixComponent,
  NzAffixModule
} from "./chunk-YO5FQYAA.js";
import {
  NzScrollService
} from "./chunk-2JNUPWPT.js";
import "./chunk-F3KMHNIL.js";
import "./chunk-TUVRXNSZ.js";
import "./chunk-QYDDKLT3.js";
import {
  NzConfigService,
  WithConfig
} from "./chunk-M622CJQC.js";
import "./chunk-PKOG7UK4.js";
import {
  fromEventOutsideAngular,
  numberAttributeWithZeroFallback
} from "./chunk-HP6B2NEN.js";
import {
  Platform,
  normalizePassiveListenerOptions
} from "./chunk-W6VE2EMK.js";
import "./chunk-AMAGFN52.js";
import {
  NgTemplateOutlet
} from "./chunk-OXRDR26M.js";
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ContentChild,
  DOCUMENT,
  DestroyRef,
  ElementRef,
  EventEmitter,
  Input,
  NgModule,
  Output,
  Renderer2,
  TemplateRef,
  ViewChild,
  ViewEncapsulation,
  booleanAttribute,
  inject,
  numberAttribute,
  setClassMetadata,
  ɵɵNgOnChangesFeature,
  ɵɵadvance,
  ɵɵattribute,
  ɵɵclassMap,
  ɵɵconditional,
  ɵɵconditionalCreate,
  ɵɵcontentQuery,
  ɵɵdefineComponent,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵelement,
  ɵɵelementEnd,
  ɵɵelementStart,
  ɵɵgetCurrentView,
  ɵɵlistener,
  ɵɵloadQuery,
  ɵɵnextContext,
  ɵɵprojection,
  ɵɵprojectionDef,
  ɵɵproperty,
  ɵɵpureFunction1,
  ɵɵqueryRefresh,
  ɵɵreference,
  ɵɵresetView,
  ɵɵrestoreView,
  ɵɵsanitizeUrl,
  ɵɵstyleMap,
  ɵɵtemplate,
  ɵɵtemplateRefExtractor,
  ɵɵtext,
  ɵɵtextInterpolate,
  ɵɵviewQuery
} from "./chunk-4XV3JIPT.js";
import {
  Subject,
  takeUntil,
  throttleTime
} from "./chunk-XZWRYGZ6.js";
import {
  __esDecorate,
  __runInitializers
} from "./chunk-DP5J3HDO.js";
import "./chunk-KWSTWQNB.js";

// node_modules/ng-zorro-antd/fesm2022/ng-zorro-antd-anchor.mjs
var _c0 = ["ink"];
var _c1 = ["*"];
var _c2 = (a0) => ({
  "ant-anchor-wrapper-horizontal": a0
});
var _c3 = (a0) => ({
  "ant-anchor-fixed": a0
});
function NzAnchorComponent_Conditional_0_ng_template_1_Template(rf, ctx) {
}
function NzAnchorComponent_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "nz-affix", 2);
    ɵɵtemplate(1, NzAnchorComponent_Conditional_0_ng_template_1_Template, 0, 0, "ng-template", 3);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    const content_r2 = ɵɵreference(3);
    ɵɵproperty("nzOffsetTop", ctx_r0.nzOffsetTop)("nzTarget", ctx_r0.container);
    ɵɵadvance();
    ɵɵproperty("ngTemplateOutlet", content_r2);
  }
}
function NzAnchorComponent_Conditional_1_ng_template_0_Template(rf, ctx) {
}
function NzAnchorComponent_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzAnchorComponent_Conditional_1_ng_template_0_Template, 0, 0, "ng-template", 3);
  }
  if (rf & 2) {
    ɵɵnextContext();
    const content_r2 = ɵɵreference(3);
    ɵɵproperty("ngTemplateOutlet", content_r2);
  }
}
function NzAnchorComponent_ng_template_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 4)(1, "div", 5)(2, "div", 6);
    ɵɵelement(3, "div", 7, 1);
    ɵɵelementEnd();
    ɵɵprojection(5);
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const ctx_r0 = ɵɵnextContext();
    ɵɵstyleMap(ctx_r0.wrapperStyle);
    ɵɵclassMap(ɵɵpureFunction1(6, _c2, ctx_r0.nzDirection === "horizontal"));
    ɵɵadvance();
    ɵɵclassMap(ɵɵpureFunction1(8, _c3, !ctx_r0.nzAffix && !ctx_r0.nzShowInkInFixed));
  }
}
var _c4 = ["nzTemplate"];
var _c5 = ["linkTitle"];
function NzAnchorLinkComponent_Conditional_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "span");
    ɵɵtext(1);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵtextInterpolate(ctx_r1.titleStr);
  }
}
function NzAnchorLinkComponent_Conditional_3_ng_template_0_Template(rf, ctx) {
}
function NzAnchorLinkComponent_Conditional_3_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵtemplate(0, NzAnchorLinkComponent_Conditional_3_ng_template_0_Template, 0, 0, "ng-template", 2);
  }
  if (rf & 2) {
    const ctx_r1 = ɵɵnextContext();
    ɵɵproperty("ngTemplateOutlet", ctx_r1.titleTpl || ctx_r1.nzTemplate);
  }
}
function NzAnchorLinkComponent_Conditional_4_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵprojection(0);
  }
}
function getOffsetTop(element, container) {
  if (!element || !element.getClientRects().length) {
    return 0;
  }
  const rect = element.getBoundingClientRect();
  if (rect.width || rect.height) {
    if (container === window) {
      const documentElement = element.ownerDocument.documentElement;
      return rect.top - documentElement.clientTop;
    }
    return rect.top - container.getBoundingClientRect().top;
  }
  return rect.top;
}
var VISIBLE_CLASSNAME = "ant-anchor-ink-ball-visible";
var NZ_CONFIG_MODULE_NAME = "anchor";
var sharpMatcherRegx = /#([^#]+)$/;
var passiveEventListenerOptions = normalizePassiveListenerOptions({
  passive: true
});
var NzAnchorComponent = (() => {
  let _nzShowInkInFixed_decorators;
  let _nzShowInkInFixed_initializers = [];
  let _nzShowInkInFixed_extraInitializers = [];
  let _nzBounds_decorators;
  let _nzBounds_initializers = [];
  let _nzBounds_extraInitializers = [];
  let _nzOffsetTop_decorators;
  let _nzOffsetTop_initializers = [];
  let _nzOffsetTop_extraInitializers = [];
  let _nzTargetOffset_decorators;
  let _nzTargetOffset_initializers = [];
  let _nzTargetOffset_extraInitializers = [];
  return class NzAnchorComponent2 {
    static {
      const _metadata = typeof Symbol === "function" && Symbol.metadata ? /* @__PURE__ */ Object.create(null) : void 0;
      _nzShowInkInFixed_decorators = [WithConfig()];
      _nzBounds_decorators = [WithConfig()];
      _nzOffsetTop_decorators = [WithConfig()];
      _nzTargetOffset_decorators = [WithConfig()];
      __esDecorate(null, null, _nzShowInkInFixed_decorators, {
        kind: "field",
        name: "nzShowInkInFixed",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzShowInkInFixed" in obj,
          get: (obj) => obj.nzShowInkInFixed,
          set: (obj, value) => {
            obj.nzShowInkInFixed = value;
          }
        },
        metadata: _metadata
      }, _nzShowInkInFixed_initializers, _nzShowInkInFixed_extraInitializers);
      __esDecorate(null, null, _nzBounds_decorators, {
        kind: "field",
        name: "nzBounds",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzBounds" in obj,
          get: (obj) => obj.nzBounds,
          set: (obj, value) => {
            obj.nzBounds = value;
          }
        },
        metadata: _metadata
      }, _nzBounds_initializers, _nzBounds_extraInitializers);
      __esDecorate(null, null, _nzOffsetTop_decorators, {
        kind: "field",
        name: "nzOffsetTop",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzOffsetTop" in obj,
          get: (obj) => obj.nzOffsetTop,
          set: (obj, value) => {
            obj.nzOffsetTop = value;
          }
        },
        metadata: _metadata
      }, _nzOffsetTop_initializers, _nzOffsetTop_extraInitializers);
      __esDecorate(null, null, _nzTargetOffset_decorators, {
        kind: "field",
        name: "nzTargetOffset",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzTargetOffset" in obj,
          get: (obj) => obj.nzTargetOffset,
          set: (obj, value) => {
            obj.nzTargetOffset = value;
          }
        },
        metadata: _metadata
      }, _nzTargetOffset_initializers, _nzTargetOffset_extraInitializers);
      if (_metadata) Object.defineProperty(this, Symbol.metadata, {
        enumerable: true,
        configurable: true,
        writable: true,
        value: _metadata
      });
    }
    nzConfigService = inject(NzConfigService);
    scrollSrv = inject(NzScrollService);
    cdr = inject(ChangeDetectorRef);
    platform = inject(Platform);
    renderer = inject(Renderer2);
    doc = inject(DOCUMENT);
    destroyRef = inject(DestroyRef);
    _nzModuleName = NZ_CONFIG_MODULE_NAME;
    ink;
    nzAffix = true;
    nzShowInkInFixed = __runInitializers(this, _nzShowInkInFixed_initializers, false);
    nzBounds = (__runInitializers(this, _nzShowInkInFixed_extraInitializers), __runInitializers(this, _nzBounds_initializers, 5));
    nzOffsetTop = (__runInitializers(this, _nzBounds_extraInitializers), __runInitializers(this, _nzOffsetTop_initializers, void 0));
    nzTargetOffset = (__runInitializers(this, _nzOffsetTop_extraInitializers), __runInitializers(this, _nzTargetOffset_initializers, void 0));
    nzContainer = __runInitializers(this, _nzTargetOffset_extraInitializers);
    nzCurrentAnchor;
    nzDirection = "vertical";
    nzClick = new EventEmitter();
    nzChange = new EventEmitter();
    nzScroll = new EventEmitter();
    visible = false;
    wrapperStyle = {
      "max-height": "100vh"
    };
    container;
    activeLink;
    links = [];
    animating = false;
    destroy$ = new Subject();
    handleScrollTimeoutID;
    constructor() {
      this.destroyRef.onDestroy(() => {
        clearTimeout(this.handleScrollTimeoutID);
        this.destroy$.next(true);
        this.destroy$.complete();
      });
    }
    registerLink(link) {
      this.links.push(link);
    }
    unregisterLink(link) {
      this.links.splice(this.links.indexOf(link), 1);
    }
    getContainer() {
      return this.container || window;
    }
    ngAfterViewInit() {
      this.registerScrollEvent();
    }
    registerScrollEvent() {
      if (!this.platform.isBrowser) {
        return;
      }
      this.destroy$.next(true);
      fromEventOutsideAngular(this.getContainer(), "scroll", passiveEventListenerOptions).pipe(throttleTime(50), takeUntil(this.destroy$)).subscribe(() => this.handleScroll());
      this.handleScrollTimeoutID = setTimeout(() => this.handleScroll());
    }
    handleScroll() {
      if (typeof document === "undefined" || this.animating) {
        return;
      }
      const sections = [];
      const offsetTop = this.nzTargetOffset ? this.nzTargetOffset : this.nzOffsetTop || 0;
      const scope = offsetTop + this.nzBounds;
      this.links.forEach((comp) => {
        const sharpLinkMatch = sharpMatcherRegx.exec(comp.nzHref.toString());
        if (!sharpLinkMatch) {
          return;
        }
        const target = this.doc.getElementById(sharpLinkMatch[1]);
        if (target) {
          const top = getOffsetTop(target, this.getContainer());
          if (top < scope) {
            sections.push({
              top,
              comp
            });
          }
        }
      });
      this.visible = !!sections.length;
      if (!this.visible) {
        this.clearActive();
        this.cdr.detectChanges();
      } else {
        const maxSection = sections.reduce((prev, curr) => curr.top > prev.top ? curr : prev);
        this.handleActive(maxSection.comp);
      }
      this.setVisible();
    }
    clearActive() {
      this.links.forEach((i) => {
        i.unsetActive();
      });
    }
    setActive(comp) {
      const originalActiveLink = this.activeLink;
      const targetComp = this.nzCurrentAnchor && this.links.find((n) => n.nzHref === this.nzCurrentAnchor) || comp;
      if (!targetComp) return;
      targetComp.setActive();
      const linkNode = targetComp.getLinkTitleElement();
      if (this.nzDirection === "vertical") {
        this.ink.nativeElement.style.top = `${linkNode.offsetTop + linkNode.clientHeight / 2 - 4.5}px`;
      } else {
        this.ink.nativeElement.style.left = `${linkNode.offsetLeft + linkNode.clientWidth / 2}px`;
      }
      this.activeLink = (comp || targetComp).nzHref;
      if (originalActiveLink !== this.activeLink) {
        this.nzChange.emit(this.activeLink);
      }
    }
    handleActive(comp) {
      this.clearActive();
      this.setActive(comp);
      this.visible = true;
      this.setVisible();
      this.nzScroll.emit(comp);
    }
    setVisible() {
      if (this.ink) {
        const visible = this.visible;
        if (visible) {
          this.renderer.addClass(this.ink.nativeElement, VISIBLE_CLASSNAME);
        } else {
          this.renderer.removeClass(this.ink.nativeElement, VISIBLE_CLASSNAME);
        }
      }
    }
    handleScrollTo(linkComp) {
      const el = this.doc.querySelector(linkComp.nzHref);
      if (!el) {
        return;
      }
      this.animating = true;
      const containerScrollTop = this.scrollSrv.getScroll(this.getContainer());
      const elOffsetTop = getOffsetTop(el, this.getContainer());
      let targetScrollTop = containerScrollTop + elOffsetTop;
      targetScrollTop -= this.nzTargetOffset !== void 0 ? this.nzTargetOffset : this.nzOffsetTop || 0;
      this.scrollSrv.scrollTo(this.getContainer(), targetScrollTop, {
        callback: () => {
          this.animating = false;
          this.handleActive(linkComp);
        }
      });
      this.nzClick.emit(linkComp.nzHref);
    }
    ngOnChanges(changes) {
      const {
        nzOffsetTop,
        nzContainer,
        nzCurrentAnchor
      } = changes;
      if (nzOffsetTop) {
        this.wrapperStyle = {
          "max-height": `calc(100vh - ${this.nzOffsetTop}px)`
        };
      }
      if (nzContainer) {
        const container = this.nzContainer;
        this.container = typeof container === "string" ? this.doc.querySelector(container) : container;
        this.registerScrollEvent();
      }
      if (nzCurrentAnchor) {
        this.setActive();
      }
    }
    static ɵfac = function NzAnchorComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || NzAnchorComponent2)();
    };
    static ɵcmp = ɵɵdefineComponent({
      type: NzAnchorComponent2,
      selectors: [["nz-anchor"]],
      viewQuery: function NzAnchorComponent_Query(rf, ctx) {
        if (rf & 1) {
          ɵɵviewQuery(_c0, 5);
        }
        if (rf & 2) {
          let _t;
          ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.ink = _t.first);
        }
      },
      inputs: {
        nzAffix: [2, "nzAffix", "nzAffix", booleanAttribute],
        nzShowInkInFixed: [2, "nzShowInkInFixed", "nzShowInkInFixed", booleanAttribute],
        nzBounds: [2, "nzBounds", "nzBounds", numberAttribute],
        nzOffsetTop: [2, "nzOffsetTop", "nzOffsetTop", numberAttributeWithZeroFallback],
        nzTargetOffset: [2, "nzTargetOffset", "nzTargetOffset", numberAttributeWithZeroFallback],
        nzContainer: "nzContainer",
        nzCurrentAnchor: "nzCurrentAnchor",
        nzDirection: "nzDirection"
      },
      outputs: {
        nzClick: "nzClick",
        nzChange: "nzChange",
        nzScroll: "nzScroll"
      },
      exportAs: ["nzAnchor"],
      features: [ɵɵNgOnChangesFeature],
      ngContentSelectors: _c1,
      decls: 4,
      vars: 1,
      consts: [["content", ""], ["ink", ""], [3, "nzOffsetTop", "nzTarget"], [3, "ngTemplateOutlet"], [1, "ant-anchor-wrapper"], [1, "ant-anchor"], [1, "ant-anchor-ink"], [1, "ant-anchor-ink-ball"]],
      template: function NzAnchorComponent_Template(rf, ctx) {
        if (rf & 1) {
          ɵɵprojectionDef();
          ɵɵconditionalCreate(0, NzAnchorComponent_Conditional_0_Template, 2, 3, "nz-affix", 2)(1, NzAnchorComponent_Conditional_1_Template, 1, 1, null, 3);
          ɵɵtemplate(2, NzAnchorComponent_ng_template_2_Template, 6, 10, "ng-template", null, 0, ɵɵtemplateRefExtractor);
        }
        if (rf & 2) {
          ɵɵconditional(ctx.nzAffix ? 0 : 1);
        }
      },
      dependencies: [NgTemplateOutlet, NzAffixModule, NzAffixComponent],
      encapsulation: 2,
      changeDetection: 0
    });
  };
})();
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzAnchorComponent, [{
    type: Component,
    args: [{
      selector: "nz-anchor",
      exportAs: "nzAnchor",
      imports: [NgTemplateOutlet, NzAffixModule],
      template: `
    @if (nzAffix) {
      <nz-affix [nzOffsetTop]="nzOffsetTop" [nzTarget]="container">
        <ng-template [ngTemplateOutlet]="content"></ng-template>
      </nz-affix>
    } @else {
      <ng-template [ngTemplateOutlet]="content"></ng-template>
    }

    <ng-template #content>
      <div
        class="ant-anchor-wrapper"
        [class]="{ 'ant-anchor-wrapper-horizontal': nzDirection === 'horizontal' }"
        [style]="wrapperStyle"
      >
        <div class="ant-anchor" [class]="{ 'ant-anchor-fixed': !nzAffix && !nzShowInkInFixed }">
          <div class="ant-anchor-ink">
            <div class="ant-anchor-ink-ball" #ink></div>
          </div>
          <ng-content></ng-content>
        </div>
      </div>
    </ng-template>
  `,
      encapsulation: ViewEncapsulation.None,
      changeDetection: ChangeDetectionStrategy.OnPush
    }]
  }], () => [], {
    ink: [{
      type: ViewChild,
      args: ["ink", {
        static: false
      }]
    }],
    nzAffix: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzShowInkInFixed: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzBounds: [{
      type: Input,
      args: [{
        transform: numberAttribute
      }]
    }],
    nzOffsetTop: [{
      type: Input,
      args: [{
        transform: numberAttributeWithZeroFallback
      }]
    }],
    nzTargetOffset: [{
      type: Input,
      args: [{
        transform: numberAttributeWithZeroFallback
      }]
    }],
    nzContainer: [{
      type: Input
    }],
    nzCurrentAnchor: [{
      type: Input
    }],
    nzDirection: [{
      type: Input
    }],
    nzClick: [{
      type: Output
    }],
    nzChange: [{
      type: Output
    }],
    nzScroll: [{
      type: Output
    }]
  });
})();
var NzAnchorLinkComponent = class _NzAnchorLinkComponent {
  elementRef = inject(ElementRef);
  anchorComp = inject(NzAnchorComponent);
  platform = inject(Platform);
  renderer = inject(Renderer2);
  destroyRef = inject(DestroyRef);
  nzHref = "#";
  nzTarget;
  titleStr = "";
  titleTpl;
  nzDirection = "vertical";
  set nzTitle(value) {
    if (value instanceof TemplateRef) {
      this.titleStr = null;
      this.titleTpl = value;
    } else {
      this.titleStr = value;
    }
  }
  nzTemplate;
  linkTitle;
  constructor() {
    this.destroyRef.onDestroy(() => {
      this.anchorComp.unregisterLink(this);
    });
  }
  ngOnInit() {
    this.anchorComp.registerLink(this);
    this.nzDirection = this.anchorComp.nzDirection;
  }
  getLinkTitleElement() {
    return this.linkTitle.nativeElement;
  }
  setActive() {
    this.renderer.addClass(this.elementRef.nativeElement, "ant-anchor-link-active");
  }
  unsetActive() {
    this.renderer.removeClass(this.elementRef.nativeElement, "ant-anchor-link-active");
  }
  goToClick(e) {
    e.preventDefault();
    e.stopPropagation();
    if (this.platform.isBrowser) {
      this.anchorComp.handleScrollTo(this);
    }
  }
  static ɵfac = function NzAnchorLinkComponent_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzAnchorLinkComponent)();
  };
  static ɵcmp = ɵɵdefineComponent({
    type: _NzAnchorLinkComponent,
    selectors: [["nz-link"]],
    contentQueries: function NzAnchorLinkComponent_ContentQueries(rf, ctx, dirIndex) {
      if (rf & 1) {
        ɵɵcontentQuery(dirIndex, _c4, 5);
      }
      if (rf & 2) {
        let _t;
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.nzTemplate = _t.first);
      }
    },
    viewQuery: function NzAnchorLinkComponent_Query(rf, ctx) {
      if (rf & 1) {
        ɵɵviewQuery(_c5, 5);
      }
      if (rf & 2) {
        let _t;
        ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.linkTitle = _t.first);
      }
    },
    hostAttrs: [1, "ant-anchor-link"],
    inputs: {
      nzHref: "nzHref",
      nzTarget: "nzTarget",
      nzTitle: "nzTitle"
    },
    exportAs: ["nzLink"],
    ngContentSelectors: _c1,
    decls: 5,
    vars: 5,
    consts: [["linkTitle", ""], [1, "ant-anchor-link-title", 3, "click", "href", "target"], [3, "ngTemplateOutlet"]],
    template: function NzAnchorLinkComponent_Template(rf, ctx) {
      if (rf & 1) {
        const _r1 = ɵɵgetCurrentView();
        ɵɵprojectionDef();
        ɵɵelementStart(0, "a", 1, 0);
        ɵɵlistener("click", function NzAnchorLinkComponent_Template_a_click_0_listener($event) {
          ɵɵrestoreView(_r1);
          return ɵɵresetView(ctx.goToClick($event));
        });
        ɵɵconditionalCreate(2, NzAnchorLinkComponent_Conditional_2_Template, 2, 1, "span")(3, NzAnchorLinkComponent_Conditional_3_Template, 1, 1, null, 2);
        ɵɵelementEnd();
        ɵɵconditionalCreate(4, NzAnchorLinkComponent_Conditional_4_Template, 1, 0);
      }
      if (rf & 2) {
        ɵɵproperty("href", ctx.nzHref, ɵɵsanitizeUrl)("target", ctx.nzTarget);
        ɵɵattribute("title", ctx.titleStr);
        ɵɵadvance(2);
        ɵɵconditional(ctx.titleStr ? 2 : 3);
        ɵɵadvance(2);
        ɵɵconditional(ctx.nzDirection === "vertical" ? 4 : -1);
      }
    },
    dependencies: [NgTemplateOutlet],
    encapsulation: 2,
    changeDetection: 0
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzAnchorLinkComponent, [{
    type: Component,
    args: [{
      selector: "nz-link",
      exportAs: "nzLink",
      imports: [NgTemplateOutlet],
      template: `
    <a
      #linkTitle
      class="ant-anchor-link-title"
      [href]="nzHref"
      [attr.title]="titleStr"
      [target]="nzTarget"
      (click)="goToClick($event)"
    >
      @if (titleStr) {
        <span>{{ titleStr }}</span>
      } @else {
        <ng-template [ngTemplateOutlet]="titleTpl || nzTemplate" />
      }
    </a>
    @if (nzDirection === 'vertical') {
      <ng-content></ng-content>
    }
  `,
      encapsulation: ViewEncapsulation.None,
      changeDetection: ChangeDetectionStrategy.OnPush,
      host: {
        class: "ant-anchor-link"
      }
    }]
  }], () => [], {
    nzHref: [{
      type: Input
    }],
    nzTarget: [{
      type: Input
    }],
    nzTitle: [{
      type: Input
    }],
    nzTemplate: [{
      type: ContentChild,
      args: ["nzTemplate", {
        static: false
      }]
    }],
    linkTitle: [{
      type: ViewChild,
      args: ["linkTitle"]
    }]
  });
})();
var NzAnchorModule = class _NzAnchorModule {
  static ɵfac = function NzAnchorModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzAnchorModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NzAnchorModule,
    imports: [NzAnchorComponent, NzAnchorLinkComponent],
    exports: [NzAnchorComponent, NzAnchorLinkComponent]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [NzAnchorComponent]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzAnchorModule, [{
    type: NgModule,
    args: [{
      exports: [NzAnchorComponent, NzAnchorLinkComponent],
      imports: [NzAnchorComponent, NzAnchorLinkComponent]
    }]
  }], null, null);
})();
export {
  NzAnchorComponent,
  NzAnchorLinkComponent,
  NzAnchorModule
};
//# sourceMappingURL=ng-zorro-antd_anchor.js.map
