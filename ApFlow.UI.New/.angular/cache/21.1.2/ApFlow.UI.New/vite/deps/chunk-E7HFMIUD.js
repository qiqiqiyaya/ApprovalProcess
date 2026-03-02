import {
  NzResizeObserver
} from "./chunk-BT773X5Q.js";
import {
  NzScrollService
} from "./chunk-KQTSQZPL.js";
import {
  Platform
} from "./chunk-W6VE2EMK.js";
import {
  WithConfig
} from "./chunk-4U5FUHDN.js";
import {
  takeUntilDestroyed
} from "./chunk-PKOG7UK4.js";
import {
  getStyleAsText,
  numberAttributeWithZeroFallback,
  shallowEqual
} from "./chunk-SJ4NT3YH.js";
import {
  Directionality
} from "./chunk-AMAGFN52.js";
import {
  ChangeDetectionStrategy,
  Component,
  DOCUMENT,
  DestroyRef,
  ElementRef,
  Input,
  NgModule,
  NgZone,
  Output,
  Renderer2,
  ViewChild,
  ViewEncapsulation,
  effect,
  inject,
  output,
  setClassMetadata,
  ɵɵNgOnChangesFeature,
  ɵɵdefineComponent,
  ɵɵdefineInjector,
  ɵɵdefineNgModule,
  ɵɵdomElementEnd,
  ɵɵdomElementStart,
  ɵɵloadQuery,
  ɵɵprojection,
  ɵɵprojectionDef,
  ɵɵqueryRefresh,
  ɵɵviewQuery
} from "./chunk-4XV3JIPT.js";
import {
  ReplaySubject,
  Subscription,
  fromEvent,
  map,
  merge,
  throttleTime
} from "./chunk-XZWRYGZ6.js";
import {
  __esDecorate,
  __runInitializers
} from "./chunk-DP5J3HDO.js";
import {
  __spreadProps,
  __spreadValues
} from "./chunk-KWSTWQNB.js";

// node_modules/ng-zorro-antd/fesm2022/ng-zorro-antd-affix.mjs
var _c0 = ["fixedEl"];
var _c1 = ["*"];
var AffixRespondEvents;
(function(AffixRespondEvents2) {
  AffixRespondEvents2["resize"] = "resize";
  AffixRespondEvents2["scroll"] = "scroll";
  AffixRespondEvents2["touchstart"] = "touchstart";
  AffixRespondEvents2["touchmove"] = "touchmove";
  AffixRespondEvents2["touchend"] = "touchend";
  AffixRespondEvents2["pageshow"] = "pageshow";
  AffixRespondEvents2["load"] = "LOAD";
})(AffixRespondEvents || (AffixRespondEvents = {}));
function isTargetWindow(target) {
  return typeof window !== "undefined" && target === window;
}
function getTargetRect(target) {
  return !isTargetWindow(target) ? target.getBoundingClientRect() : {
    top: 0,
    left: 0,
    bottom: 0
  };
}
var NZ_CONFIG_MODULE_NAME = "affix";
var NZ_AFFIX_CLS_PREFIX = "ant-affix";
var NZ_AFFIX_DEFAULT_SCROLL_TIME = 20;
var NOOP_EVENT = {};
var NzAffixComponent = (() => {
  let _nzOffsetTop_decorators;
  let _nzOffsetTop_initializers = [];
  let _nzOffsetTop_extraInitializers = [];
  let _nzOffsetBottom_decorators;
  let _nzOffsetBottom_initializers = [];
  let _nzOffsetBottom_extraInitializers = [];
  return class NzAffixComponent2 {
    static {
      const _metadata = typeof Symbol === "function" && Symbol.metadata ? /* @__PURE__ */ Object.create(null) : void 0;
      _nzOffsetTop_decorators = [WithConfig()];
      _nzOffsetBottom_decorators = [WithConfig()];
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
      __esDecorate(null, null, _nzOffsetBottom_decorators, {
        kind: "field",
        name: "nzOffsetBottom",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzOffsetBottom" in obj,
          get: (obj) => obj.nzOffsetBottom,
          set: (obj, value) => {
            obj.nzOffsetBottom = value;
          }
        },
        metadata: _metadata
      }, _nzOffsetBottom_initializers, _nzOffsetBottom_extraInitializers);
      if (_metadata) Object.defineProperty(this, Symbol.metadata, {
        enumerable: true,
        configurable: true,
        writable: true,
        value: _metadata
      });
    }
    scrollSrv = inject(NzScrollService);
    ngZone = inject(NgZone);
    platform = inject(Platform);
    renderer = inject(Renderer2);
    nzResizeObserver = inject(NzResizeObserver);
    directionality = inject(Directionality);
    destroyRef = inject(DestroyRef);
    document = inject(DOCUMENT);
    placeholderNode = inject(ElementRef).nativeElement;
    _nzModuleName = NZ_CONFIG_MODULE_NAME;
    fixedEl;
    nzTarget;
    nzOffsetTop = __runInitializers(this, _nzOffsetTop_initializers, void 0);
    nzOffsetBottom = (__runInitializers(this, _nzOffsetTop_extraInitializers), __runInitializers(this, _nzOffsetBottom_initializers, void 0));
    nzChange = (__runInitializers(this, _nzOffsetBottom_extraInitializers), output());
    affixStyle;
    placeholderStyle;
    positionChangeSubscription = Subscription.EMPTY;
    offsetChanged$ = new ReplaySubject(1);
    timeout;
    get target() {
      const el = this.nzTarget;
      return (typeof el === "string" ? this.document.querySelector(el) : el) || window;
    }
    constructor() {
      effect(() => {
        this.directionality.valueSignal();
        this.registerListeners();
        this.updatePosition(NOOP_EVENT);
      });
      this.destroyRef.onDestroy(() => {
        this.removeListeners();
      });
    }
    ngOnChanges(changes) {
      const {
        nzOffsetBottom,
        nzOffsetTop,
        nzTarget
      } = changes;
      if (nzOffsetBottom || nzOffsetTop) {
        this.offsetChanged$.next();
      }
      if (nzTarget) {
        this.registerListeners();
      }
    }
    registerListeners() {
      if (!this.platform.isBrowser) {
        return;
      }
      this.removeListeners();
      const el = this.target === window ? this.document.body : this.target;
      this.positionChangeSubscription = this.ngZone.runOutsideAngular(() => merge(...Object.keys(AffixRespondEvents).map((evName) => fromEvent(this.target, evName)), this.offsetChanged$.pipe(map(() => NOOP_EVENT)), this.nzResizeObserver.observe(el)).pipe(throttleTime(NZ_AFFIX_DEFAULT_SCROLL_TIME, void 0, {
        trailing: true
      }), takeUntilDestroyed(this.destroyRef)).subscribe((e) => this.updatePosition(e)));
      this.timeout = setTimeout(() => this.updatePosition(NOOP_EVENT));
    }
    removeListeners() {
      clearTimeout(this.timeout);
      this.positionChangeSubscription.unsubscribe();
    }
    getOffset(element, target) {
      const elemRect = element.getBoundingClientRect();
      const targetRect = getTargetRect(target);
      const scrollTop = this.scrollSrv.getScroll(target, true);
      const scrollLeft = this.scrollSrv.getScroll(target, false);
      const docElem = this.document.body;
      const clientTop = docElem.clientTop || 0;
      const clientLeft = docElem.clientLeft || 0;
      return {
        top: elemRect.top - targetRect.top + scrollTop - clientTop,
        left: elemRect.left - targetRect.left + scrollLeft - clientLeft,
        width: elemRect.width,
        height: elemRect.height
      };
    }
    setAffixStyle(e, affixStyle) {
      const originalAffixStyle = this.affixStyle;
      if (e.type === "scroll" && originalAffixStyle && affixStyle && this.target === window) {
        return;
      }
      if (shallowEqual(originalAffixStyle, affixStyle)) {
        return;
      }
      const fixed = !!affixStyle;
      const wrapEl = this.fixedEl.nativeElement;
      this.renderer.setStyle(wrapEl, "cssText", getStyleAsText(affixStyle));
      this.affixStyle = affixStyle;
      if (fixed) {
        wrapEl.classList.add(NZ_AFFIX_CLS_PREFIX);
      } else {
        wrapEl.classList.remove(NZ_AFFIX_CLS_PREFIX);
      }
      this.updateRtlClass();
      if (affixStyle && !originalAffixStyle || !affixStyle && originalAffixStyle) {
        this.nzChange.emit(fixed);
      }
    }
    setPlaceholderStyle(placeholderStyle) {
      const originalPlaceholderStyle = this.placeholderStyle;
      if (shallowEqual(placeholderStyle, originalPlaceholderStyle)) {
        return;
      }
      this.renderer.setStyle(this.placeholderNode, "cssText", getStyleAsText(placeholderStyle));
      this.placeholderStyle = placeholderStyle;
    }
    syncPlaceholderStyle(e) {
      if (!this.affixStyle) {
        return;
      }
      this.renderer.setStyle(this.placeholderNode, "cssText", "");
      this.placeholderStyle = void 0;
      const styleObj = {
        width: this.placeholderNode.offsetWidth,
        height: this.fixedEl.nativeElement.offsetHeight
      };
      this.setAffixStyle(e, __spreadValues(__spreadValues({}, this.affixStyle), styleObj));
      this.setPlaceholderStyle(styleObj);
    }
    updatePosition(e) {
      if (!this.platform.isBrowser) {
        return;
      }
      const targetNode = this.target;
      let offsetTop = this.nzOffsetTop;
      const scrollTop = this.scrollSrv.getScroll(targetNode, true);
      const elemOffset = this.getOffset(this.placeholderNode, targetNode);
      const fixedNode = this.fixedEl.nativeElement;
      const elemSize = {
        width: fixedNode.offsetWidth,
        height: fixedNode.offsetHeight
      };
      const offsetMode = {
        top: false,
        bottom: false
      };
      if (typeof offsetTop !== "number" && typeof this.nzOffsetBottom !== "number") {
        offsetMode.top = true;
        offsetTop = 0;
      } else {
        offsetMode.top = typeof offsetTop === "number";
        offsetMode.bottom = typeof this.nzOffsetBottom === "number";
      }
      const targetRect = getTargetRect(targetNode);
      const targetInnerHeight = targetNode.innerHeight || targetNode.clientHeight;
      if (scrollTop >= elemOffset.top - offsetTop && offsetMode.top) {
        const width = elemOffset.width;
        const top = targetRect.top + offsetTop;
        this.setAffixStyle(e, {
          position: "fixed",
          top,
          left: targetRect.left + elemOffset.left,
          width
        });
        this.setPlaceholderStyle({
          width,
          height: elemSize.height
        });
      } else if (scrollTop <= elemOffset.top + elemSize.height + this.nzOffsetBottom - targetInnerHeight && offsetMode.bottom) {
        const targetBottomOffset = targetNode === window ? 0 : window.innerHeight - targetRect.bottom;
        const width = elemOffset.width;
        this.setAffixStyle(e, {
          position: "fixed",
          bottom: targetBottomOffset + this.nzOffsetBottom,
          left: targetRect.left + elemOffset.left,
          width
        });
        this.setPlaceholderStyle({
          width,
          height: elemOffset.height
        });
      } else {
        if (e.type === AffixRespondEvents.resize && this.affixStyle && this.affixStyle.position === "fixed" && this.placeholderNode.offsetWidth) {
          this.setAffixStyle(e, __spreadProps(__spreadValues({}, this.affixStyle), {
            width: this.placeholderNode.offsetWidth
          }));
        } else {
          this.setAffixStyle(e);
        }
        this.setPlaceholderStyle();
      }
      if (e.type === "resize") {
        this.syncPlaceholderStyle(e);
      }
    }
    updateRtlClass() {
      const wrapEl = this.fixedEl.nativeElement;
      if (this.directionality.valueSignal() === "rtl" && wrapEl.classList.contains(NZ_AFFIX_CLS_PREFIX)) {
        wrapEl.classList.add(`${NZ_AFFIX_CLS_PREFIX}-rtl`);
      } else {
        wrapEl.classList.remove(`${NZ_AFFIX_CLS_PREFIX}-rtl`);
      }
    }
    static ɵfac = function NzAffixComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || NzAffixComponent2)();
    };
    static ɵcmp = ɵɵdefineComponent({
      type: NzAffixComponent2,
      selectors: [["nz-affix"]],
      viewQuery: function NzAffixComponent_Query(rf, ctx) {
        if (rf & 1) {
          ɵɵviewQuery(_c0, 7);
        }
        if (rf & 2) {
          let _t;
          ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.fixedEl = _t.first);
        }
      },
      inputs: {
        nzTarget: "nzTarget",
        nzOffsetTop: [2, "nzOffsetTop", "nzOffsetTop", numberAttributeWithZeroFallback],
        nzOffsetBottom: [2, "nzOffsetBottom", "nzOffsetBottom", numberAttributeWithZeroFallback]
      },
      outputs: {
        nzChange: "nzChange"
      },
      exportAs: ["nzAffix"],
      features: [ɵɵNgOnChangesFeature],
      ngContentSelectors: _c1,
      decls: 3,
      vars: 0,
      consts: [["fixedEl", ""]],
      template: function NzAffixComponent_Template(rf, ctx) {
        if (rf & 1) {
          ɵɵprojectionDef();
          ɵɵdomElementStart(0, "div", null, 0);
          ɵɵprojection(2);
          ɵɵdomElementEnd();
        }
      },
      encapsulation: 2,
      changeDetection: 0
    });
  };
})();
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzAffixComponent, [{
    type: Component,
    args: [{
      selector: "nz-affix",
      exportAs: "nzAffix",
      template: `
    <div #fixedEl>
      <ng-content></ng-content>
    </div>
  `,
      changeDetection: ChangeDetectionStrategy.OnPush,
      encapsulation: ViewEncapsulation.None
    }]
  }], () => [], {
    fixedEl: [{
      type: ViewChild,
      args: ["fixedEl", {
        static: true
      }]
    }],
    nzTarget: [{
      type: Input
    }],
    nzOffsetTop: [{
      type: Input,
      args: [{
        transform: numberAttributeWithZeroFallback
      }]
    }],
    nzOffsetBottom: [{
      type: Input,
      args: [{
        transform: numberAttributeWithZeroFallback
      }]
    }],
    nzChange: [{
      type: Output,
      args: ["nzChange"]
    }]
  });
})();
var NzAffixModule = class _NzAffixModule {
  static ɵfac = function NzAffixModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzAffixModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NzAffixModule,
    imports: [NzAffixComponent],
    exports: [NzAffixComponent]
  });
  static ɵinj = ɵɵdefineInjector({});
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzAffixModule, [{
    type: NgModule,
    args: [{
      exports: [NzAffixComponent],
      imports: [NzAffixComponent]
    }]
  }], null, null);
})();

export {
  NzAffixComponent,
  NzAffixModule
};
//# sourceMappingURL=chunk-E7HFMIUD.js.map
