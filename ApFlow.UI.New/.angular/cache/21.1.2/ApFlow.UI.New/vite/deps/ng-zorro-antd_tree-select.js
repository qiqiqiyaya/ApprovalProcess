import {
  NzTreeBase,
  NzTreeBaseService,
  NzTreeComponent,
  NzTreeHigherOrderServiceToken,
  NzTreeModule
} from "./chunk-6E5KCIUX.js";
import {
  NzSelectArrowComponent,
  NzSelectClearComponent,
  NzSelectItemComponent,
  NzSelectModule,
  NzSelectPlaceholderComponent,
  NzSelectSearchComponent
} from "./chunk-UC3LVZWF.js";
import {
  NzEmbedEmptyComponent,
  NzEmptyModule
} from "./chunk-U2FVUJJP.js";
import {
  FocusMonitor
} from "./chunk-CQLW6MKY.js";
import "./chunk-AWU3GB3T.js";
import "./chunk-6DTLTA2W.js";
import {
  NzFormItemFeedbackIconComponent,
  NzFormNoStatusService,
  NzFormStatusService
} from "./chunk-2AFHA5UD.js";
import "./chunk-GYMNEEDE.js";
import {
  NzConnectedOverlayDirective,
  NzOverlayModule,
  POSITION_MAP
} from "./chunk-LMQQNRJH.js";
import {
  CdkConnectedOverlay,
  CdkOverlayOrigin
} from "./chunk-GLLB55ZV.js";
import "./chunk-ZHVRDCSU.js";
import "./chunk-4OODPXLP.js";
import "./chunk-RJK3RDQK.js";
import {
  BACKSPACE,
  ESCAPE,
  TAB
} from "./chunk-B7XDWOSB.js";
import {
  NzNoAnimationDirective,
  slideAnimationEnter,
  slideAnimationLeave
} from "./chunk-SPYBVWE7.js";
import {
  requestAnimationFrame
} from "./chunk-QYDDKLT3.js";
import {
  NZ_SPACE_COMPACT_ITEM_TYPE,
  NZ_SPACE_COMPACT_SIZE,
  NzSpaceCompactItemDirective
} from "./chunk-BLMJUYRV.js";
import {
  NzStringTemplateOutletDirective
} from "./chunk-CWBX6YEP.js";
import {
  Directionality
} from "./chunk-AMAGFN52.js";
import "./chunk-HQM66OPU.js";
import "./chunk-72DKPDI6.js";
import {
  _getEventTarget
} from "./chunk-W6VE2EMK.js";
import {
  WithConfig,
  onConfigChangeEventForComponent
} from "./chunk-M622CJQC.js";
import {
  takeUntilDestroyed
} from "./chunk-PKOG7UK4.js";
import {
  getStatusClassNames,
  isNotNil
} from "./chunk-HP6B2NEN.js";
import {
  NG_VALUE_ACCESSOR
} from "./chunk-YAIECLDM.js";
import "./chunk-6OM3666T.js";
import "./chunk-BQ76GOFF.js";
import "./chunk-N2XNJ4XE.js";
import {
  SlicePipe
} from "./chunk-OXRDR26M.js";
import {
  ChangeDetectorRef,
  Component,
  ContentChild,
  DestroyRef,
  ElementRef,
  EventEmitter,
  Injectable,
  Input,
  NgModule,
  Output,
  Renderer2,
  ViewChild,
  booleanAttribute,
  computed,
  forwardRef,
  inject,
  numberAttribute,
  setClassMetadata,
  signal,
  ɵɵHostDirectivesFeature,
  ɵɵInheritDefinitionFeature,
  ɵɵNgOnChangesFeature,
  ɵɵProvidersFeature,
  ɵɵadvance,
  ɵɵanimateEnter,
  ɵɵanimateLeave,
  ɵɵclassMap,
  ɵɵclassProp,
  ɵɵconditional,
  ɵɵconditionalCreate,
  ɵɵcontentQuery,
  ɵɵdefineComponent,
  ɵɵdefineInjectable,
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
  ɵɵloadQuery,
  ɵɵnextContext,
  ɵɵpipe,
  ɵɵpipeBind2,
  ɵɵpipeBind3,
  ɵɵproperty,
  ɵɵpureFunction0,
  ɵɵqueryRefresh,
  ɵɵreference,
  ɵɵrepeater,
  ɵɵrepeaterCreate,
  ɵɵresetView,
  ɵɵrestoreView,
  ɵɵstyleMap,
  ɵɵstyleProp,
  ɵɵtemplate,
  ɵɵtemplateRefExtractor,
  ɵɵtext,
  ɵɵtextInterpolate,
  ɵɵviewQuery
} from "./chunk-4XV3JIPT.js";
import {
  Subject,
  combineLatest,
  distinctUntilChanged,
  filter,
  map,
  merge,
  of,
  startWith,
  tap,
  withLatestFrom
} from "./chunk-XZWRYGZ6.js";
import {
  __esDecorate,
  __runInitializers
} from "./chunk-DP5J3HDO.js";
import "./chunk-KWSTWQNB.js";

// node_modules/ng-zorro-antd/fesm2022/ng-zorro-antd-tree-select.mjs
var _c0 = ["nzTreeTemplate"];
var _c1 = ["treeRef"];
var _c2 = () => [];
var _forTrack0 = ($index, $item) => $item.key;
function NzTreeSelectComponent_ng_template_0_Conditional_3_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "span", 11);
    ɵɵelement(1, "nz-embed-empty", 12);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵproperty("nzComponentName", "tree-select")("specificContent", ctx_r2.nzNotFoundContent);
  }
}
function NzTreeSelectComponent_ng_template_0_Template(rf, ctx) {
  if (rf & 1) {
    const _r2 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 9);
    ɵɵanimateLeave(function NzTreeSelectComponent_ng_template_0_Template_animateleave_cb() {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.slideAnimationLeave());
    });
    ɵɵanimateEnter(function NzTreeSelectComponent_ng_template_0_Template_animateenter_cb() {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.slideAnimationEnter());
    });
    ɵɵelementStart(1, "nz-tree", 10, 1);
    ɵɵlistener("nzExpandChange", function NzTreeSelectComponent_ng_template_0_Template_nz_tree_nzExpandChange_1_listener($event) {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.onExpandedKeysChange($event));
    })("nzClick", function NzTreeSelectComponent_ng_template_0_Template_nz_tree_nzClick_1_listener($event) {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.nzTreeClick.emit($event));
    })("nzCheckedKeysChange", function NzTreeSelectComponent_ng_template_0_Template_nz_tree_nzCheckedKeysChange_1_listener() {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.updateSelectedNodes());
    })("nzSelectedKeysChange", function NzTreeSelectComponent_ng_template_0_Template_nz_tree_nzSelectedKeysChange_1_listener() {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.updateSelectedNodes());
    })("nzCheckboxChange", function NzTreeSelectComponent_ng_template_0_Template_nz_tree_nzCheckboxChange_1_listener($event) {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.nzTreeCheckboxChange.emit($event));
    })("nzSearchValueChange", function NzTreeSelectComponent_ng_template_0_Template_nz_tree_nzSearchValueChange_1_listener($event) {
      ɵɵrestoreView(_r2);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.setSearchValues($event));
    });
    ɵɵelementEnd();
    ɵɵconditionalCreate(3, NzTreeSelectComponent_ng_template_0_Conditional_3_Template, 2, 2, "span", 11);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext();
    ɵɵstyleMap(ctx_r2.nzDropdownStyle);
    ɵɵclassMap(ctx_r2.dropdownClassName);
    ɵɵclassProp("ant-select-dropdown-placement-bottomLeft", ctx_r2.dropdownPosition === "bottom")("ant-select-dropdown-placement-topLeft", ctx_r2.dropdownPosition === "top")("ant-tree-select-dropdown-rtl", ctx_r2.dir === "rtl");
    ɵɵproperty("nzNoAnimation", !!(ctx_r2.noAnimation == null ? null : ctx_r2.noAnimation.nzNoAnimation == null ? null : ctx_r2.noAnimation.nzNoAnimation()))("dir", ctx_r2.dir);
    ɵɵadvance();
    ɵɵproperty("hidden", ctx_r2.isNotFound)("nzData", ctx_r2.nzNodes)("nzMultiple", ctx_r2.nzMultiple)("nzSearchValue", ctx_r2.inputValue)("nzHideUnMatched", ctx_r2.nzHideUnMatched)("nzShowIcon", ctx_r2.nzShowIcon)("nzCheckable", ctx_r2.nzCheckable)("nzAsyncData", ctx_r2.nzAsyncData)("nzShowExpand", ctx_r2.nzShowExpand)("nzShowLine", ctx_r2.nzShowLine)("nzExpandedIcon", ctx_r2.nzExpandedIcon)("nzExpandAll", ctx_r2.nzDefaultExpandAll)("nzExpandedKeys", ctx_r2.expandedKeys)("nzCheckedKeys", ctx_r2.nzCheckable ? ctx_r2.value : ɵɵpureFunction0(34, _c2))("nzSelectedKeys", !ctx_r2.nzCheckable ? ctx_r2.value : ɵɵpureFunction0(35, _c2))("nzTreeTemplate", ctx_r2.treeTemplate)("nzCheckStrictly", ctx_r2.nzCheckStrictly)("nzVirtualItemSize", ctx_r2.nzVirtualItemSize)("nzVirtualMaxBufferPx", ctx_r2.nzVirtualMaxBufferPx)("nzVirtualMinBufferPx", ctx_r2.nzVirtualMinBufferPx)("nzVirtualHeight", ctx_r2.nzVirtualHeight);
    ɵɵadvance(2);
    ɵɵconditional(ctx_r2.nzNodes.length === 0 || ctx_r2.isNotFound ? 3 : -1);
  }
}
function NzTreeSelectComponent_Conditional_2_ng_container_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementContainerStart(0);
    ɵɵtext(1);
    ɵɵelementContainerEnd();
  }
  if (rf & 2) {
    const prefix_r4 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵtextInterpolate(prefix_r4);
  }
}
function NzTreeSelectComponent_Conditional_2_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 4);
    ɵɵtemplate(1, NzTreeSelectComponent_Conditional_2_ng_container_1_Template, 2, 1, "ng-container", 13);
    ɵɵelementEnd();
  }
  if (rf & 2) {
    ɵɵadvance();
    ɵɵproperty("nzStringTemplateOutlet", ctx);
  }
}
function NzTreeSelectComponent_Conditional_4_For_2_Template(rf, ctx) {
  if (rf & 1) {
    const _r6 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 14)(1, "nz-select-item", 17);
    ɵɵlistener("delete", function NzTreeSelectComponent_Conditional_4_For_2_Template_nz_select_item_delete_1_listener() {
      const node_r7 = ɵɵrestoreView(_r6).$implicit;
      const ctx_r2 = ɵɵnextContext(2);
      return ɵɵresetView(ctx_r2.removeSelected(node_r7, true));
    });
    ɵɵelementEnd()();
  }
  if (rf & 2) {
    const node_r7 = ctx.$implicit;
    const ctx_r2 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵproperty("disabled", node_r7.isDisabled || ctx_r2.nzDisabled)("label", ctx_r2.nzDisplayWith(node_r7));
  }
}
function NzTreeSelectComponent_Conditional_4_Conditional_4_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelementStart(0, "div", 14);
    ɵɵelement(1, "nz-select-item", 18);
    ɵɵpipe(2, "slice");
    ɵɵelementEnd();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext(2);
    ɵɵadvance();
    ɵɵproperty("contentTemplateOutlet", ctx_r2.nzMaxTagPlaceholder)("contentTemplateOutletContext", ɵɵpipeBind2(2, 3, ctx_r2.selectedNodes, ctx_r2.nzMaxTagCount))("label", "+ " + (ctx_r2.selectedNodes.length - ctx_r2.nzMaxTagCount) + " ...");
  }
}
function NzTreeSelectComponent_Conditional_4_Template(rf, ctx) {
  if (rf & 1) {
    const _r5 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "div", 6);
    ɵɵrepeaterCreate(1, NzTreeSelectComponent_Conditional_4_For_2_Template, 2, 2, "div", 14, _forTrack0);
    ɵɵpipe(3, "slice");
    ɵɵconditionalCreate(4, NzTreeSelectComponent_Conditional_4_Conditional_4_Template, 3, 6, "div", 14);
    ɵɵelementStart(5, "div", 15)(6, "nz-select-search", 16);
    ɵɵlistener("keydown", function NzTreeSelectComponent_Conditional_4_Template_nz_select_search_keydown_6_listener($event) {
      ɵɵrestoreView(_r5);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.onKeyDownInput($event));
    })("isComposingChange", function NzTreeSelectComponent_Conditional_4_Template_nz_select_search_isComposingChange_6_listener($event) {
      ɵɵrestoreView(_r5);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.isComposingChange($event));
    })("valueChange", function NzTreeSelectComponent_Conditional_4_Template_nz_select_search_valueChange_6_listener($event) {
      ɵɵrestoreView(_r5);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.setInputValue($event));
    });
    ɵɵelementEnd()()();
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext();
    ɵɵadvance();
    ɵɵrepeater(ɵɵpipeBind3(3, 7, ctx_r2.selectedNodes, 0, ctx_r2.nzMaxTagCount));
    ɵɵadvance(3);
    ɵɵconditional(ctx_r2.selectedNodes.length > ctx_r2.nzMaxTagCount ? 4 : -1);
    ɵɵadvance(2);
    ɵɵproperty("nzId", ctx_r2.nzId)("showInput", ctx_r2.nzShowSearch)("value", ctx_r2.inputValue)("mirrorSync", true)("disabled", ctx_r2.nzDisabled)("focusTrigger", ctx_r2.nzOpen);
  }
}
function NzTreeSelectComponent_Conditional_5_Conditional_1_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-select-item", 19);
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext(2);
    ɵɵproperty("label", ctx_r2.nzDisplayWith(ctx_r2.selectedNodes[0]));
  }
}
function NzTreeSelectComponent_Conditional_5_Template(rf, ctx) {
  if (rf & 1) {
    const _r8 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "nz-select-search", 16);
    ɵɵlistener("keydown", function NzTreeSelectComponent_Conditional_5_Template_nz_select_search_keydown_0_listener($event) {
      ɵɵrestoreView(_r8);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.onKeyDownInput($event));
    })("isComposingChange", function NzTreeSelectComponent_Conditional_5_Template_nz_select_search_isComposingChange_0_listener($event) {
      ɵɵrestoreView(_r8);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.isComposingChange($event));
    })("valueChange", function NzTreeSelectComponent_Conditional_5_Template_nz_select_search_valueChange_0_listener($event) {
      ɵɵrestoreView(_r8);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.setInputValue($event));
    });
    ɵɵelementEnd();
    ɵɵconditionalCreate(1, NzTreeSelectComponent_Conditional_5_Conditional_1_Template, 1, 1, "nz-select-item", 19);
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext();
    ɵɵproperty("nzId", ctx_r2.nzId)("showInput", ctx_r2.nzShowSearch)("value", ctx_r2.inputValue)("mirrorSync", false)("disabled", ctx_r2.nzDisabled)("focusTrigger", ctx_r2.nzOpen);
    ɵɵadvance();
    ɵɵconditional(ctx_r2.selectedNodes.length === 1 && !ctx_r2.isComposing && ctx_r2.inputValue === "" ? 1 : -1);
  }
}
function NzTreeSelectComponent_Conditional_6_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-select-placeholder", 20);
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext();
    ɵɵstyleProp("display", ctx_r2.placeHolderDisplay);
    ɵɵproperty("placeholder", ctx_r2.nzPlaceHolder);
  }
}
function NzTreeSelectComponent_ng_template_8_Conditional_0_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵelement(0, "nz-form-item-feedback-icon", 21);
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext(2);
    ɵɵproperty("status", ctx_r2.status);
  }
}
function NzTreeSelectComponent_ng_template_8_Template(rf, ctx) {
  if (rf & 1) {
    ɵɵconditionalCreate(0, NzTreeSelectComponent_ng_template_8_Conditional_0_Template, 1, 1, "nz-form-item-feedback-icon", 21);
  }
  if (rf & 2) {
    const ctx_r2 = ɵɵnextContext();
    ɵɵconditional(ctx_r2.hasFeedback && !!ctx_r2.status ? 0 : -1);
  }
}
function NzTreeSelectComponent_Conditional_10_Template(rf, ctx) {
  if (rf & 1) {
    const _r9 = ɵɵgetCurrentView();
    ɵɵelementStart(0, "nz-select-clear", 22);
    ɵɵlistener("clear", function NzTreeSelectComponent_Conditional_10_Template_nz_select_clear_clear_0_listener() {
      ɵɵrestoreView(_r9);
      const ctx_r2 = ɵɵnextContext();
      return ɵɵresetView(ctx_r2.onClearSelection());
    });
    ɵɵelementEnd();
  }
}
var NzTreeSelectService = class _NzTreeSelectService extends NzTreeBaseService {
  static ɵfac = /* @__PURE__ */ (() => {
    let ɵNzTreeSelectService_BaseFactory;
    return function NzTreeSelectService_Factory(__ngFactoryType__) {
      return (ɵNzTreeSelectService_BaseFactory || (ɵNzTreeSelectService_BaseFactory = ɵɵgetInheritedFactory(_NzTreeSelectService)))(__ngFactoryType__ || _NzTreeSelectService);
    };
  })();
  static ɵprov = ɵɵdefineInjectable({
    token: _NzTreeSelectService,
    factory: _NzTreeSelectService.ɵfac
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzTreeSelectService, [{
    type: Injectable
  }], null, null);
})();
var NZ_CONFIG_MODULE_NAME = "treeSelect";
var TREE_SELECT_DEFAULT_CLASS = "ant-select-dropdown ant-select-tree-dropdown";
var listOfPositions = [POSITION_MAP.bottomLeft, POSITION_MAP.bottomRight, POSITION_MAP.topRight, POSITION_MAP.topLeft];
var NzTreeSelectComponent = (() => {
  let _classSuper = NzTreeBase;
  let _nzDropdownMatchSelectWidth_decorators;
  let _nzDropdownMatchSelectWidth_initializers = [];
  let _nzDropdownMatchSelectWidth_extraInitializers = [];
  let _nzHideUnMatched_decorators;
  let _nzHideUnMatched_initializers = [];
  let _nzHideUnMatched_extraInitializers = [];
  let _nzShowIcon_decorators;
  let _nzShowIcon_initializers = [];
  let _nzShowIcon_extraInitializers = [];
  let _nzSize_decorators;
  let _nzSize_initializers = [];
  let _nzSize_extraInitializers = [];
  let _nzVariant_decorators;
  let _nzVariant_initializers = [];
  let _nzVariant_extraInitializers = [];
  let _nzBackdrop_decorators;
  let _nzBackdrop_initializers = [];
  let _nzBackdrop_extraInitializers = [];
  return class NzTreeSelectComponent2 extends _classSuper {
    static {
      const _metadata = typeof Symbol === "function" && Symbol.metadata ? Object.create(_classSuper[Symbol.metadata] ?? null) : void 0;
      _nzDropdownMatchSelectWidth_decorators = [WithConfig()];
      _nzHideUnMatched_decorators = [WithConfig()];
      _nzShowIcon_decorators = [WithConfig()];
      _nzSize_decorators = [WithConfig()];
      _nzVariant_decorators = [WithConfig()];
      _nzBackdrop_decorators = [WithConfig()];
      __esDecorate(null, null, _nzDropdownMatchSelectWidth_decorators, {
        kind: "field",
        name: "nzDropdownMatchSelectWidth",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzDropdownMatchSelectWidth" in obj,
          get: (obj) => obj.nzDropdownMatchSelectWidth,
          set: (obj, value) => {
            obj.nzDropdownMatchSelectWidth = value;
          }
        },
        metadata: _metadata
      }, _nzDropdownMatchSelectWidth_initializers, _nzDropdownMatchSelectWidth_extraInitializers);
      __esDecorate(null, null, _nzHideUnMatched_decorators, {
        kind: "field",
        name: "nzHideUnMatched",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzHideUnMatched" in obj,
          get: (obj) => obj.nzHideUnMatched,
          set: (obj, value) => {
            obj.nzHideUnMatched = value;
          }
        },
        metadata: _metadata
      }, _nzHideUnMatched_initializers, _nzHideUnMatched_extraInitializers);
      __esDecorate(null, null, _nzShowIcon_decorators, {
        kind: "field",
        name: "nzShowIcon",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzShowIcon" in obj,
          get: (obj) => obj.nzShowIcon,
          set: (obj, value) => {
            obj.nzShowIcon = value;
          }
        },
        metadata: _metadata
      }, _nzShowIcon_initializers, _nzShowIcon_extraInitializers);
      __esDecorate(null, null, _nzSize_decorators, {
        kind: "field",
        name: "nzSize",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzSize" in obj,
          get: (obj) => obj.nzSize,
          set: (obj, value) => {
            obj.nzSize = value;
          }
        },
        metadata: _metadata
      }, _nzSize_initializers, _nzSize_extraInitializers);
      __esDecorate(null, null, _nzVariant_decorators, {
        kind: "field",
        name: "nzVariant",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzVariant" in obj,
          get: (obj) => obj.nzVariant,
          set: (obj, value) => {
            obj.nzVariant = value;
          }
        },
        metadata: _metadata
      }, _nzVariant_initializers, _nzVariant_extraInitializers);
      __esDecorate(null, null, _nzBackdrop_decorators, {
        kind: "field",
        name: "nzBackdrop",
        static: false,
        private: false,
        access: {
          has: (obj) => "nzBackdrop" in obj,
          get: (obj) => obj.nzBackdrop,
          set: (obj, value) => {
            obj.nzBackdrop = value;
          }
        },
        metadata: _metadata
      }, _nzBackdrop_initializers, _nzBackdrop_extraInitializers);
      if (_metadata) Object.defineProperty(this, Symbol.metadata, {
        enumerable: true,
        configurable: true,
        writable: true,
        value: _metadata
      });
    }
    _nzModuleName = NZ_CONFIG_MODULE_NAME;
    renderer = inject(Renderer2);
    cdr = inject(ChangeDetectorRef);
    elementRef = inject(ElementRef);
    directionality = inject(Directionality);
    focusMonitor = inject(FocusMonitor);
    destroyRef = inject(DestroyRef);
    slideAnimationEnter = slideAnimationEnter();
    slideAnimationLeave = slideAnimationLeave();
    nzId = null;
    nzAllowClear = true;
    nzShowExpand = true;
    nzShowLine = false;
    nzDropdownMatchSelectWidth = __runInitializers(this, _nzDropdownMatchSelectWidth_initializers, true);
    nzCheckable = (__runInitializers(this, _nzDropdownMatchSelectWidth_extraInitializers), false);
    nzHideUnMatched = __runInitializers(this, _nzHideUnMatched_initializers, false);
    nzShowIcon = (__runInitializers(this, _nzHideUnMatched_extraInitializers), __runInitializers(this, _nzShowIcon_initializers, false));
    nzShowSearch = (__runInitializers(this, _nzShowIcon_extraInitializers), false);
    nzDisabled = false;
    nzAsyncData = false;
    nzMultiple = false;
    nzDefaultExpandAll = false;
    nzCheckStrictly = false;
    nzVirtualItemSize = 28;
    nzVirtualMaxBufferPx = 500;
    nzVirtualMinBufferPx = 28;
    nzVirtualHeight = null;
    nzExpandedIcon;
    nzNotFoundContent;
    nzNodes = [];
    nzOpen = false;
    nzSize = __runInitializers(this, _nzSize_initializers, "default");
    nzVariant = (__runInitializers(this, _nzSize_extraInitializers), __runInitializers(this, _nzVariant_initializers, "outlined"));
    nzPlaceHolder = (__runInitializers(this, _nzVariant_extraInitializers), "");
    nzDropdownStyle = null;
    nzDropdownClassName;
    nzBackdrop = __runInitializers(this, _nzBackdrop_initializers, false);
    nzStatus = (__runInitializers(this, _nzBackdrop_extraInitializers), "");
    nzPlacement = "";
    set nzExpandedKeys(value) {
      this.expandedKeys = value;
    }
    get nzExpandedKeys() {
      return this.expandedKeys;
    }
    nzPrefix = null;
    nzSuffixIcon = null;
    nzDisplayWith = (node) => node.title;
    nzMaxTagCount;
    nzMaxTagPlaceholder = null;
    nzOpenChange = new EventEmitter();
    nzCleared = new EventEmitter();
    nzRemoved = new EventEmitter();
    nzExpandChange = new EventEmitter();
    nzTreeClick = new EventEmitter();
    nzTreeCheckboxChange = new EventEmitter();
    nzSelectSearchComponent;
    treeRef;
    cdkOverlayOrigin;
    cdkConnectedOverlay;
    nzTreeTemplate;
    nzTreeTemplateChild;
    get treeTemplate() {
      return this.nzTreeTemplate || this.nzTreeTemplateChild;
    }
    prefixCls = "ant-select";
    statusCls = {};
    status = "";
    hasFeedback = false;
    dropdownClassName = TREE_SELECT_DEFAULT_CLASS;
    triggerWidth;
    isComposing = false;
    isNotFound = false;
    focused = false;
    inputValue = "";
    dropdownPosition = "bottom";
    selectedNodes = [];
    expandedKeys = [];
    value = [];
    dir = "ltr";
    positions = [];
    finalSize = computed(() => {
      if (this.compactSize) {
        return this.compactSize();
      }
      return this.size();
    }, ...ngDevMode ? [{
      debugName: "finalSize"
    }] : []);
    size = signal(this.nzSize, ...ngDevMode ? [{
      debugName: "size"
    }] : []);
    compactSize = inject(NZ_SPACE_COMPACT_SIZE, {
      optional: true
    });
    isNzDisableFirstChange = true;
    isComposingChange$ = new Subject();
    searchValueChange$ = new Subject();
    onChange = (_value) => {
    };
    onTouched = () => {
    };
    get placeHolderDisplay() {
      return this.inputValue || this.isComposing || this.selectedNodes.length ? "none" : "block";
    }
    get isMultiple() {
      return this.nzMultiple || this.nzCheckable;
    }
    noAnimation = inject(NzNoAnimationDirective, {
      host: true,
      optional: true
    });
    nzFormStatusService = inject(NzFormStatusService, {
      optional: true
    });
    nzFormNoStatusService = inject(NzFormNoStatusService, {
      optional: true
    });
    constructor() {
      super(inject(NzTreeSelectService));
      this.destroyRef.onDestroy(() => {
        this.closeDropdown();
      });
      onConfigChangeEventForComponent(NZ_CONFIG_MODULE_NAME, () => {
        this.size.set(this.nzSize);
        this.cdr.markForCheck();
      });
    }
    ngOnInit() {
      this.size.set(this.nzSize);
      this.nzFormStatusService?.formStatusChanges.pipe(distinctUntilChanged((pre, cur) => {
        return pre.status === cur.status && pre.hasFeedback === cur.hasFeedback;
      }), withLatestFrom(this.nzFormNoStatusService ? this.nzFormNoStatusService.noFormStatus : of(false)), map(([{
        status,
        hasFeedback
      }, noStatus]) => ({
        status: noStatus ? "" : status,
        hasFeedback
      })), takeUntilDestroyed(this.destroyRef)).subscribe(({
        status,
        hasFeedback
      }) => {
        this.setStatusStyles(status, hasFeedback);
      });
      this.subscribeSelectionChange();
      this.directionality.change?.pipe(takeUntilDestroyed(this.destroyRef)).subscribe((direction) => {
        this.dir = direction;
        this.cdr.detectChanges();
      });
      this.dir = this.directionality.value;
      this.focusMonitor.monitor(this.elementRef, true).pipe(takeUntilDestroyed(this.destroyRef)).subscribe((focusOrigin) => {
        if (!focusOrigin) {
          this.focused = false;
          this.cdr.markForCheck();
          Promise.resolve().then(() => {
            this.onTouched();
          });
        } else {
          this.focused = true;
          this.cdr.markForCheck();
        }
      });
      combineLatest([this.searchValueChange$, this.isComposingChange$.pipe(startWith(false))]).pipe(takeUntilDestroyed(this.destroyRef)).subscribe(([searchValue, isComposing]) => {
        this.isComposing = isComposing;
        if (!isComposing) {
          this.inputValue = searchValue;
          this.updatePosition();
        }
      });
    }
    isComposingChange(isComposing) {
      this.isComposingChange$.next(isComposing);
    }
    setDisabledState(isDisabled) {
      this.nzDisabled = this.isNzDisableFirstChange && this.nzDisabled || isDisabled;
      this.closeDropdown();
      this.isNzDisableFirstChange = false;
    }
    setStatusStyles(status, hasFeedback) {
      this.status = status;
      this.hasFeedback = hasFeedback;
      this.cdr.markForCheck();
      this.statusCls = getStatusClassNames(this.prefixCls, status, hasFeedback);
      Object.keys(this.statusCls).forEach((status2) => {
        if (this.statusCls[status2]) {
          this.renderer.addClass(this.elementRef.nativeElement, status2);
        } else {
          this.renderer.removeClass(this.elementRef.nativeElement, status2);
        }
      });
    }
    ngOnChanges({
      nzNodes,
      nzDropdownClassName,
      nzStatus,
      nzPlacement,
      nzSize
    }) {
      if (nzNodes) {
        this.updateSelectedNodes(true);
      }
      if (nzDropdownClassName) {
        const className = this.nzDropdownClassName && this.nzDropdownClassName.trim();
        this.dropdownClassName = className ? `${TREE_SELECT_DEFAULT_CLASS} ${className}` : TREE_SELECT_DEFAULT_CLASS;
      }
      if (nzStatus) {
        this.setStatusStyles(this.nzStatus, this.hasFeedback);
      }
      if (nzPlacement && this.nzPlacement) {
        if (POSITION_MAP[this.nzPlacement]) {
          this.positions = [POSITION_MAP[this.nzPlacement]];
        }
      }
      if (nzSize) {
        this.size.set(nzSize.currentValue);
      }
    }
    writeValue(value) {
      if (isNotNil(value)) {
        if (this.isMultiple && Array.isArray(value)) {
          this.value = value;
        } else {
          this.value = [value];
        }
        this.clearSelectedNodes();
        this.updateSelectedNodes(true);
      } else {
        this.value = [];
        this.clearSelectedNodes();
        this.selectedNodes = [];
      }
      this.cdr.markForCheck();
    }
    registerOnChange(fn) {
      this.onChange = fn;
    }
    registerOnTouched(fn) {
      this.onTouched = fn;
    }
    onKeydown(event) {
      if (this.nzDisabled) {
        return;
      }
      switch (event.keyCode) {
        case ESCAPE:
          break;
        case TAB:
          this.closeDropdown();
          break;
        default:
          if (!this.nzOpen) {
            this.openDropdown();
          }
      }
    }
    trigger() {
      if (this.nzDisabled || !this.nzDisabled && this.nzOpen) {
        this.closeDropdown();
      } else {
        this.openDropdown();
      }
    }
    openDropdown() {
      if (!this.nzDisabled) {
        this.nzOpen = true;
        this.nzOpenChange.emit(this.nzOpen);
        this.updateCdkConnectedOverlayStatus();
        if (this.nzShowSearch || this.isMultiple) {
          this.focusOnInput();
        }
      }
    }
    closeDropdown() {
      Promise.resolve().then(() => this.onTouched());
      this.nzOpen = false;
      this.inputValue = "";
      this.isNotFound = false;
      this.nzOpenChange.emit(this.nzOpen);
      this.cdr.markForCheck();
    }
    onKeyDownInput(e) {
      const keyCode = e.keyCode;
      const eventTarget = e.target;
      if (this.isMultiple && !eventTarget.value && keyCode === BACKSPACE) {
        e.preventDefault();
        if (this.selectedNodes.length) {
          const removeNode = this.selectedNodes[this.selectedNodes.length - 1];
          if (removeNode && !removeNode.isDisabled) {
            this.removeSelected(removeNode);
          }
        }
      }
    }
    onExpandedKeysChange(value) {
      this.nzExpandChange.emit(value);
      this.expandedKeys = [...value.keys];
    }
    setInputValue(value) {
      this.searchValueChange$.next(value);
    }
    removeSelected(node, emit = true) {
      node.isSelected = false;
      node.isChecked = false;
      if (this.nzCheckable) {
        this.nzTreeService.conduct(node, this.nzCheckStrictly);
      } else {
        this.nzTreeService.setSelectedNodeList(node, this.nzMultiple);
      }
      if (emit) {
        this.nzRemoved.emit(node);
      }
    }
    focusOnInput() {
      if (this.nzSelectSearchComponent) {
        this.nzSelectSearchComponent.focus();
      }
    }
    subscribeSelectionChange() {
      merge(this.nzTreeClick.pipe(tap((event) => {
        const node = event.node;
        if (this.nzCheckable && !node.isDisabled && !node.isDisableCheckbox) {
          node.isChecked = !node.isChecked;
          node.isHalfChecked = false;
          if (!this.nzCheckStrictly) {
            this.nzTreeService.conduct(node);
          }
        }
        if (this.nzCheckable) {
          node.isSelected = false;
        }
      }), filter((event) => {
        const node = event.node;
        return this.nzCheckable ? !node.isDisabled && !node.isDisableCheckbox : !node.isDisabled && node.isSelectable;
      })), this.nzCheckable ? this.nzTreeCheckboxChange.asObservable() : of(), this.nzCleared, this.nzRemoved).pipe(takeUntilDestroyed(this.destroyRef)).subscribe(() => {
        this.updateSelectedNodes();
        const value = this.selectedNodes.map((node) => node.key);
        this.value = [...value];
        if (this.nzShowSearch || this.isMultiple) {
          this.inputValue = "";
          this.isNotFound = false;
        }
        if (this.isMultiple) {
          this.onChange(value);
          this.focusOnInput();
          this.updatePosition();
        } else {
          this.closeDropdown();
          this.onChange(value.length ? value[0] : null);
        }
      });
    }
    updateSelectedNodes(init = false) {
      if (init) {
        const nodes = this.coerceTreeNodes(this.nzNodes);
        this.nzTreeService.isMultiple = this.isMultiple;
        this.nzTreeService.isCheckStrictly = this.nzCheckStrictly;
        this.nzTreeService.initTree(nodes);
        if (this.nzCheckable) {
          this.nzTreeService.conductCheck(this.value, this.nzCheckStrictly);
        } else {
          this.nzTreeService.conductSelectedKeys(this.value, this.isMultiple);
        }
      }
      this.selectedNodes = [...this.nzCheckable ? this.getCheckedNodeList() : this.getSelectedNodeList()].sort((a, b) => {
        const indexA = this.value.indexOf(a.key);
        const indexB = this.value.indexOf(b.key);
        if (indexA !== -1 && indexB !== -1) {
          return indexA - indexB;
        }
        if (indexA !== -1) {
          return -1;
        }
        if (indexB !== -1) {
          return 1;
        }
        return 0;
      });
    }
    updatePosition() {
      requestAnimationFrame(() => {
        this.cdkConnectedOverlay?.overlayRef?.updatePosition();
      });
    }
    onPositionChange(position) {
      this.dropdownPosition = position.connectionPair.originY;
    }
    onClearSelection() {
      this.selectedNodes.forEach((node) => {
        this.removeSelected(node, false);
      });
      this.nzCleared.emit();
    }
    onClickOutside(event) {
      const target = _getEventTarget(event);
      if (!this.elementRef.nativeElement.contains(target)) {
        this.closeDropdown();
      }
    }
    setSearchValues($event) {
      Promise.resolve().then(() => {
        this.isNotFound = (this.nzShowSearch || this.isMultiple) && !!this.inputValue && $event.matchedKeys.length === 0;
      });
    }
    updateCdkConnectedOverlayStatus() {
      if (!this.nzPlacement || !listOfPositions.includes(POSITION_MAP[this.nzPlacement])) {
        this.triggerWidth = this.cdkOverlayOrigin.elementRef.nativeElement.getBoundingClientRect().width;
      }
    }
    clearSelectedNodes() {
      this.selectedNodes.forEach((node) => {
        this.removeSelected(node, false);
      });
    }
    static ɵfac = function NzTreeSelectComponent_Factory(__ngFactoryType__) {
      return new (__ngFactoryType__ || NzTreeSelectComponent2)();
    };
    static ɵcmp = ɵɵdefineComponent({
      type: NzTreeSelectComponent2,
      selectors: [["nz-tree-select"]],
      contentQueries: function NzTreeSelectComponent_ContentQueries(rf, ctx, dirIndex) {
        if (rf & 1) {
          ɵɵcontentQuery(dirIndex, _c0, 7);
        }
        if (rf & 2) {
          let _t;
          ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.nzTreeTemplateChild = _t.first);
        }
      },
      viewQuery: function NzTreeSelectComponent_Query(rf, ctx) {
        if (rf & 1) {
          ɵɵviewQuery(NzSelectSearchComponent, 5)(_c1, 5)(CdkOverlayOrigin, 7)(CdkConnectedOverlay, 5);
        }
        if (rf & 2) {
          let _t;
          ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.nzSelectSearchComponent = _t.first);
          ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.treeRef = _t.first);
          ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.cdkOverlayOrigin = _t.first);
          ɵɵqueryRefresh(_t = ɵɵloadQuery()) && (ctx.cdkConnectedOverlay = _t.first);
        }
      },
      hostAttrs: [1, "ant-select", "ant-tree-select"],
      hostVars: 30,
      hostBindings: function NzTreeSelectComponent_HostBindings(rf, ctx) {
        if (rf & 1) {
          ɵɵlistener("click", function NzTreeSelectComponent_click_HostBindingHandler() {
            return ctx.trigger();
          })("keydown", function NzTreeSelectComponent_keydown_HostBindingHandler($event) {
            return ctx.onKeydown($event);
          });
        }
        if (rf & 2) {
          ɵɵclassProp("ant-select-in-form-item", !!ctx.nzFormStatusService)("ant-select-rtl", ctx.dir === "rtl")("ant-select-lg", ctx.finalSize() === "large")("ant-select-sm", ctx.finalSize() === "small")("ant-select-disabled", ctx.nzDisabled)("ant-select-single", !ctx.isMultiple)("ant-select-show-arrow", !ctx.isMultiple)("ant-select-show-search", !ctx.isMultiple)("ant-select-borderless", ctx.nzVariant === "borderless")("ant-select-filled", ctx.nzVariant === "filled")("ant-select-underlined", ctx.nzVariant === "underlined")("ant-select-multiple", ctx.isMultiple)("ant-select-allow-clear", ctx.nzAllowClear)("ant-select-open", ctx.nzOpen)("ant-select-focused", ctx.nzOpen || ctx.focused);
        }
      },
      inputs: {
        nzId: "nzId",
        nzAllowClear: [2, "nzAllowClear", "nzAllowClear", booleanAttribute],
        nzShowExpand: [2, "nzShowExpand", "nzShowExpand", booleanAttribute],
        nzShowLine: [2, "nzShowLine", "nzShowLine", booleanAttribute],
        nzDropdownMatchSelectWidth: [2, "nzDropdownMatchSelectWidth", "nzDropdownMatchSelectWidth", booleanAttribute],
        nzCheckable: [2, "nzCheckable", "nzCheckable", booleanAttribute],
        nzHideUnMatched: [2, "nzHideUnMatched", "nzHideUnMatched", booleanAttribute],
        nzShowIcon: [2, "nzShowIcon", "nzShowIcon", booleanAttribute],
        nzShowSearch: [2, "nzShowSearch", "nzShowSearch", booleanAttribute],
        nzDisabled: [2, "nzDisabled", "nzDisabled", booleanAttribute],
        nzAsyncData: [2, "nzAsyncData", "nzAsyncData", booleanAttribute],
        nzMultiple: [2, "nzMultiple", "nzMultiple", booleanAttribute],
        nzDefaultExpandAll: [2, "nzDefaultExpandAll", "nzDefaultExpandAll", booleanAttribute],
        nzCheckStrictly: [2, "nzCheckStrictly", "nzCheckStrictly", booleanAttribute],
        nzVirtualItemSize: "nzVirtualItemSize",
        nzVirtualMaxBufferPx: "nzVirtualMaxBufferPx",
        nzVirtualMinBufferPx: "nzVirtualMinBufferPx",
        nzVirtualHeight: "nzVirtualHeight",
        nzExpandedIcon: "nzExpandedIcon",
        nzNotFoundContent: "nzNotFoundContent",
        nzNodes: "nzNodes",
        nzOpen: "nzOpen",
        nzSize: "nzSize",
        nzVariant: "nzVariant",
        nzPlaceHolder: "nzPlaceHolder",
        nzDropdownStyle: "nzDropdownStyle",
        nzDropdownClassName: "nzDropdownClassName",
        nzBackdrop: "nzBackdrop",
        nzStatus: "nzStatus",
        nzPlacement: "nzPlacement",
        nzExpandedKeys: "nzExpandedKeys",
        nzPrefix: "nzPrefix",
        nzSuffixIcon: "nzSuffixIcon",
        nzDisplayWith: "nzDisplayWith",
        nzMaxTagCount: [2, "nzMaxTagCount", "nzMaxTagCount", numberAttribute],
        nzMaxTagPlaceholder: "nzMaxTagPlaceholder",
        nzTreeTemplate: "nzTreeTemplate"
      },
      outputs: {
        nzOpenChange: "nzOpenChange",
        nzCleared: "nzCleared",
        nzRemoved: "nzRemoved",
        nzExpandChange: "nzExpandChange",
        nzTreeClick: "nzTreeClick",
        nzTreeCheckboxChange: "nzTreeCheckboxChange"
      },
      exportAs: ["nzTreeSelect"],
      features: [ɵɵProvidersFeature([NzTreeSelectService, {
        provide: NZ_SPACE_COMPACT_ITEM_TYPE,
        useValue: "select"
      }, {
        provide: NzTreeHigherOrderServiceToken,
        useExisting: NzTreeSelectService
      }, {
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => NzTreeSelectComponent2),
        multi: true
      }]), ɵɵHostDirectivesFeature([NzSpaceCompactItemDirective]), ɵɵInheritDefinitionFeature, ɵɵNgOnChangesFeature],
      decls: 11,
      vars: 16,
      consts: [["feedbackIconTpl", ""], ["treeRef", ""], ["cdkConnectedOverlay", "", "nzConnectedOverlay", "", 3, "overlayOutsideClick", "detach", "positionChange", "cdkConnectedOverlayHasBackdrop", "cdkConnectedOverlayOrigin", "cdkConnectedOverlayPositions", "cdkConnectedOverlayOpen", "cdkConnectedOverlayTransformOriginOn", "cdkConnectedOverlayMinWidth", "cdkConnectedOverlayWidth"], ["cdkOverlayOrigin", "", 1, "ant-select-selector"], [1, "ant-select-prefix"], [1, "ant-select-selection-wrap"], [1, "ant-select-selection-overflow"], [3, "placeholder", "display"], [3, "showArrow", "search", "suffixIcon", "feedbackIcon"], [3, "nzNoAnimation", "dir"], ["nzNoAnimation", "", "nzSelectMode", "", "nzBlockNode", "", 3, "nzExpandChange", "nzClick", "nzCheckedKeysChange", "nzSelectedKeysChange", "nzCheckboxChange", "nzSearchValueChange", "hidden", "nzData", "nzMultiple", "nzSearchValue", "nzHideUnMatched", "nzShowIcon", "nzCheckable", "nzAsyncData", "nzShowExpand", "nzShowLine", "nzExpandedIcon", "nzExpandAll", "nzExpandedKeys", "nzCheckedKeys", "nzSelectedKeys", "nzTreeTemplate", "nzCheckStrictly", "nzVirtualItemSize", "nzVirtualMaxBufferPx", "nzVirtualMinBufferPx", "nzVirtualHeight"], [1, "ant-select-not-found"], [3, "nzComponentName", "specificContent"], [4, "nzStringTemplateOutlet"], [1, "ant-select-selection-overflow-item"], [1, "ant-select-selection-overflow-item", "ant-select-selection-overflow-item-suffix"], [3, "keydown", "isComposingChange", "valueChange", "nzId", "showInput", "value", "mirrorSync", "disabled", "focusTrigger"], ["deletable", "", "displayLabelInHtml", "", 3, "delete", "disabled", "label"], [3, "contentTemplateOutlet", "contentTemplateOutletContext", "label"], ["displayLabelInHtml", "", 3, "label"], [3, "placeholder"], [3, "status"], [3, "clear"]],
      template: function NzTreeSelectComponent_Template(rf, ctx) {
        if (rf & 1) {
          const _r1 = ɵɵgetCurrentView();
          ɵɵtemplate(0, NzTreeSelectComponent_ng_template_0_Template, 4, 36, "ng-template", 2);
          ɵɵlistener("overlayOutsideClick", function NzTreeSelectComponent_Template_ng_template_overlayOutsideClick_0_listener($event) {
            ɵɵrestoreView(_r1);
            return ɵɵresetView(ctx.onClickOutside($event));
          })("detach", function NzTreeSelectComponent_Template_ng_template_detach_0_listener() {
            ɵɵrestoreView(_r1);
            return ɵɵresetView(ctx.closeDropdown());
          })("positionChange", function NzTreeSelectComponent_Template_ng_template_positionChange_0_listener($event) {
            ɵɵrestoreView(_r1);
            return ɵɵresetView(ctx.onPositionChange($event));
          });
          ɵɵelementStart(1, "div", 3);
          ɵɵconditionalCreate(2, NzTreeSelectComponent_Conditional_2_Template, 2, 1, "div", 4);
          ɵɵelementStart(3, "span", 5);
          ɵɵconditionalCreate(4, NzTreeSelectComponent_Conditional_4_Template, 7, 11, "div", 6)(5, NzTreeSelectComponent_Conditional_5_Template, 2, 7);
          ɵɵconditionalCreate(6, NzTreeSelectComponent_Conditional_6_Template, 1, 3, "nz-select-placeholder", 7);
          ɵɵelementEnd();
          ɵɵelementStart(7, "nz-select-arrow", 8);
          ɵɵtemplate(8, NzTreeSelectComponent_ng_template_8_Template, 1, 1, "ng-template", null, 0, ɵɵtemplateRefExtractor);
          ɵɵelementEnd();
          ɵɵconditionalCreate(10, NzTreeSelectComponent_Conditional_10_Template, 1, 0, "nz-select-clear");
          ɵɵelementEnd();
        }
        if (rf & 2) {
          let tmp_8_0;
          const feedbackIconTpl_r10 = ɵɵreference(9);
          ɵɵproperty("cdkConnectedOverlayHasBackdrop", ctx.nzBackdrop)("cdkConnectedOverlayOrigin", ctx.cdkOverlayOrigin)("cdkConnectedOverlayPositions", ctx.nzPlacement ? ctx.positions : ɵɵpureFunction0(15, _c2))("cdkConnectedOverlayOpen", ctx.nzOpen)("cdkConnectedOverlayTransformOriginOn", ".ant-select-tree-dropdown")("cdkConnectedOverlayMinWidth", ctx.nzDropdownMatchSelectWidth ? null : ctx.triggerWidth)("cdkConnectedOverlayWidth", ctx.nzDropdownMatchSelectWidth ? ctx.triggerWidth : null);
          ɵɵadvance(2);
          ɵɵconditional((tmp_8_0 = ctx.nzPrefix) ? 2 : -1, tmp_8_0);
          ɵɵadvance(2);
          ɵɵconditional(ctx.isMultiple ? 4 : 5);
          ɵɵadvance(2);
          ɵɵconditional(ctx.nzPlaceHolder && ctx.selectedNodes.length === 0 ? 6 : -1);
          ɵɵadvance();
          ɵɵproperty("showArrow", true)("search", ctx.nzOpen && ctx.nzShowSearch)("suffixIcon", ctx.nzSuffixIcon)("feedbackIcon", feedbackIconTpl_r10);
          ɵɵadvance(3);
          ɵɵconditional(ctx.nzAllowClear && !ctx.nzDisabled && ctx.selectedNodes.length ? 10 : -1);
        }
      },
      dependencies: [NzOverlayModule, NzConnectedOverlayDirective, CdkConnectedOverlay, NzNoAnimationDirective, NzTreeModule, NzTreeComponent, NzEmptyModule, NzEmbedEmptyComponent, CdkOverlayOrigin, NzSelectModule, NzSelectArrowComponent, NzSelectClearComponent, NzSelectItemComponent, NzSelectPlaceholderComponent, NzSelectSearchComponent, NzFormItemFeedbackIconComponent, NzStringTemplateOutletDirective, SlicePipe],
      encapsulation: 2
    });
  };
})();
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzTreeSelectComponent, [{
    type: Component,
    args: [{
      selector: "nz-tree-select",
      exportAs: "nzTreeSelect",
      imports: [NzOverlayModule, CdkConnectedOverlay, NzNoAnimationDirective, NzTreeModule, NzEmptyModule, CdkOverlayOrigin, SlicePipe, NzSelectModule, NzFormItemFeedbackIconComponent, NzStringTemplateOutletDirective],
      template: `
    <ng-template
      cdkConnectedOverlay
      nzConnectedOverlay
      [cdkConnectedOverlayHasBackdrop]="nzBackdrop"
      [cdkConnectedOverlayOrigin]="cdkOverlayOrigin"
      [cdkConnectedOverlayPositions]="nzPlacement ? positions : []"
      [cdkConnectedOverlayOpen]="nzOpen"
      [cdkConnectedOverlayTransformOriginOn]="'.ant-select-tree-dropdown'"
      [cdkConnectedOverlayMinWidth]="$any(nzDropdownMatchSelectWidth ? null : triggerWidth)"
      [cdkConnectedOverlayWidth]="$any(nzDropdownMatchSelectWidth ? triggerWidth : null)"
      (overlayOutsideClick)="onClickOutside($event)"
      (detach)="closeDropdown()"
      (positionChange)="onPositionChange($event)"
    >
      <div
        [class]="dropdownClassName"
        [nzNoAnimation]="!!noAnimation?.nzNoAnimation?.()"
        [animate.enter]="slideAnimationEnter()"
        [animate.leave]="slideAnimationLeave()"
        [class.ant-select-dropdown-placement-bottomLeft]="dropdownPosition === 'bottom'"
        [class.ant-select-dropdown-placement-topLeft]="dropdownPosition === 'top'"
        [class.ant-tree-select-dropdown-rtl]="dir === 'rtl'"
        [dir]="dir"
        [style]="nzDropdownStyle"
      >
        <nz-tree
          #treeRef
          [hidden]="isNotFound"
          nzNoAnimation
          nzSelectMode
          nzBlockNode
          [nzData]="nzNodes"
          [nzMultiple]="nzMultiple"
          [nzSearchValue]="inputValue"
          [nzHideUnMatched]="nzHideUnMatched"
          [nzShowIcon]="nzShowIcon"
          [nzCheckable]="nzCheckable"
          [nzAsyncData]="nzAsyncData"
          [nzShowExpand]="nzShowExpand"
          [nzShowLine]="nzShowLine"
          [nzExpandedIcon]="nzExpandedIcon"
          [nzExpandAll]="nzDefaultExpandAll"
          [nzExpandedKeys]="expandedKeys"
          [nzCheckedKeys]="nzCheckable ? value : []"
          [nzSelectedKeys]="!nzCheckable ? value : []"
          [nzTreeTemplate]="treeTemplate"
          [nzCheckStrictly]="nzCheckStrictly"
          [nzVirtualItemSize]="nzVirtualItemSize"
          [nzVirtualMaxBufferPx]="nzVirtualMaxBufferPx"
          [nzVirtualMinBufferPx]="nzVirtualMinBufferPx"
          [nzVirtualHeight]="nzVirtualHeight"
          (nzExpandChange)="onExpandedKeysChange($event)"
          (nzClick)="nzTreeClick.emit($event)"
          (nzCheckedKeysChange)="updateSelectedNodes()"
          (nzSelectedKeysChange)="updateSelectedNodes()"
          (nzCheckboxChange)="nzTreeCheckboxChange.emit($event)"
          (nzSearchValueChange)="setSearchValues($event)"
        ></nz-tree>
        @if (nzNodes.length === 0 || isNotFound) {
          <span class="ant-select-not-found">
            <nz-embed-empty [nzComponentName]="'tree-select'" [specificContent]="nzNotFoundContent" />
          </span>
        }
      </div>
    </ng-template>

    <div cdkOverlayOrigin class="ant-select-selector">
      @if (nzPrefix; as prefix) {
        <div class="ant-select-prefix">
          <ng-container *nzStringTemplateOutlet="prefix">{{ prefix }}</ng-container>
        </div>
      }

      <span class="ant-select-selection-wrap">
        @if (isMultiple) {
          <div class="ant-select-selection-overflow">
            @for (node of selectedNodes | slice: 0 : nzMaxTagCount; track node.key) {
              <div class="ant-select-selection-overflow-item">
                <nz-select-item
                  deletable
                  [disabled]="node.isDisabled || nzDisabled"
                  [label]="nzDisplayWith(node)"
                  displayLabelInHtml
                  (delete)="removeSelected(node, true)"
                />
              </div>
            }
            @if (selectedNodes.length > nzMaxTagCount) {
              <div class="ant-select-selection-overflow-item">
                <nz-select-item
                  [contentTemplateOutlet]="nzMaxTagPlaceholder"
                  [contentTemplateOutletContext]="selectedNodes | slice: nzMaxTagCount"
                  [label]="'+ ' + (selectedNodes.length - nzMaxTagCount) + ' ...'"
                />
              </div>
            }
            <div class="ant-select-selection-overflow-item ant-select-selection-overflow-item-suffix">
              <nz-select-search
                [nzId]="nzId"
                [showInput]="nzShowSearch"
                (keydown)="onKeyDownInput($event)"
                (isComposingChange)="isComposingChange($event)"
                (valueChange)="setInputValue($event)"
                [value]="inputValue"
                [mirrorSync]="true"
                [disabled]="nzDisabled"
                [focusTrigger]="nzOpen"
              />
            </div>
          </div>
        } @else {
          <nz-select-search
            [nzId]="nzId"
            [showInput]="nzShowSearch"
            (keydown)="onKeyDownInput($event)"
            (isComposingChange)="isComposingChange($event)"
            (valueChange)="setInputValue($event)"
            [value]="inputValue"
            [mirrorSync]="false"
            [disabled]="nzDisabled"
            [focusTrigger]="nzOpen"
          />
          @if (selectedNodes.length === 1 && !isComposing && inputValue === '') {
            <nz-select-item [label]="nzDisplayWith(selectedNodes[0])" displayLabelInHtml />
          }
        }

        @if (nzPlaceHolder && selectedNodes.length === 0) {
          <nz-select-placeholder [placeholder]="nzPlaceHolder" [style.display]="placeHolderDisplay" />
        }
      </span>

      <nz-select-arrow
        [showArrow]="true"
        [search]="nzOpen && nzShowSearch"
        [suffixIcon]="nzSuffixIcon"
        [feedbackIcon]="feedbackIconTpl"
      >
        <ng-template #feedbackIconTpl>
          @if (hasFeedback && !!status) {
            <nz-form-item-feedback-icon [status]="status" />
          }
        </ng-template>
      </nz-select-arrow>

      @if (nzAllowClear && !nzDisabled && selectedNodes.length) {
        <nz-select-clear (clear)="onClearSelection()" />
      }
    </div>
  `,
      providers: [NzTreeSelectService, {
        provide: NZ_SPACE_COMPACT_ITEM_TYPE,
        useValue: "select"
      }, {
        provide: NzTreeHigherOrderServiceToken,
        useExisting: NzTreeSelectService
      }, {
        provide: NG_VALUE_ACCESSOR,
        useExisting: forwardRef(() => NzTreeSelectComponent),
        multi: true
      }],
      host: {
        class: "ant-select ant-tree-select",
        "[class.ant-select-in-form-item]": "!!nzFormStatusService",
        "[class.ant-select-rtl]": 'dir==="rtl"',
        "[class.ant-select-lg]": 'finalSize() === "large"',
        "[class.ant-select-sm]": 'finalSize() === "small"',
        "[class.ant-select-disabled]": "nzDisabled",
        "[class.ant-select-single]": "!isMultiple",
        "[class.ant-select-show-arrow]": "!isMultiple",
        "[class.ant-select-show-search]": "!isMultiple",
        "[class.ant-select-borderless]": 'nzVariant === "borderless"',
        "[class.ant-select-filled]": 'nzVariant === "filled"',
        "[class.ant-select-underlined]": 'nzVariant === "underlined"',
        "[class.ant-select-multiple]": "isMultiple",
        "[class.ant-select-allow-clear]": "nzAllowClear",
        "[class.ant-select-open]": "nzOpen",
        "[class.ant-select-focused]": "nzOpen || focused",
        "(click)": "trigger()",
        "(keydown)": "onKeydown($event)"
      },
      hostDirectives: [NzSpaceCompactItemDirective]
    }]
  }], () => [], {
    nzId: [{
      type: Input
    }],
    nzAllowClear: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzShowExpand: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzShowLine: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzDropdownMatchSelectWidth: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzCheckable: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzHideUnMatched: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzShowIcon: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzShowSearch: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzDisabled: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzAsyncData: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzMultiple: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzDefaultExpandAll: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzCheckStrictly: [{
      type: Input,
      args: [{
        transform: booleanAttribute
      }]
    }],
    nzVirtualItemSize: [{
      type: Input
    }],
    nzVirtualMaxBufferPx: [{
      type: Input
    }],
    nzVirtualMinBufferPx: [{
      type: Input
    }],
    nzVirtualHeight: [{
      type: Input
    }],
    nzExpandedIcon: [{
      type: Input
    }],
    nzNotFoundContent: [{
      type: Input
    }],
    nzNodes: [{
      type: Input
    }],
    nzOpen: [{
      type: Input
    }],
    nzSize: [{
      type: Input
    }],
    nzVariant: [{
      type: Input
    }],
    nzPlaceHolder: [{
      type: Input
    }],
    nzDropdownStyle: [{
      type: Input
    }],
    nzDropdownClassName: [{
      type: Input
    }],
    nzBackdrop: [{
      type: Input
    }],
    nzStatus: [{
      type: Input
    }],
    nzPlacement: [{
      type: Input
    }],
    nzExpandedKeys: [{
      type: Input
    }],
    nzPrefix: [{
      type: Input
    }],
    nzSuffixIcon: [{
      type: Input
    }],
    nzDisplayWith: [{
      type: Input
    }],
    nzMaxTagCount: [{
      type: Input,
      args: [{
        transform: numberAttribute
      }]
    }],
    nzMaxTagPlaceholder: [{
      type: Input
    }],
    nzOpenChange: [{
      type: Output
    }],
    nzCleared: [{
      type: Output
    }],
    nzRemoved: [{
      type: Output
    }],
    nzExpandChange: [{
      type: Output
    }],
    nzTreeClick: [{
      type: Output
    }],
    nzTreeCheckboxChange: [{
      type: Output
    }],
    nzSelectSearchComponent: [{
      type: ViewChild,
      args: [NzSelectSearchComponent, {
        static: false
      }]
    }],
    treeRef: [{
      type: ViewChild,
      args: ["treeRef", {
        static: false
      }]
    }],
    cdkOverlayOrigin: [{
      type: ViewChild,
      args: [CdkOverlayOrigin, {
        static: true
      }]
    }],
    cdkConnectedOverlay: [{
      type: ViewChild,
      args: [CdkConnectedOverlay, {
        static: false
      }]
    }],
    nzTreeTemplate: [{
      type: Input
    }],
    nzTreeTemplateChild: [{
      type: ContentChild,
      args: ["nzTreeTemplate", {
        static: true
      }]
    }]
  });
})();
var NzTreeSelectModule = class _NzTreeSelectModule {
  static ɵfac = function NzTreeSelectModule_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _NzTreeSelectModule)();
  };
  static ɵmod = ɵɵdefineNgModule({
    type: _NzTreeSelectModule,
    imports: [NzTreeSelectComponent],
    exports: [NzTreeSelectComponent]
  });
  static ɵinj = ɵɵdefineInjector({
    imports: [NzTreeSelectComponent]
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(NzTreeSelectModule, [{
    type: NgModule,
    args: [{
      imports: [NzTreeSelectComponent],
      exports: [NzTreeSelectComponent]
    }]
  }], null, null);
})();
export {
  NzTreeSelectComponent,
  NzTreeSelectModule,
  NzTreeSelectService
};
//# sourceMappingURL=ng-zorro-antd_tree-select.js.map
