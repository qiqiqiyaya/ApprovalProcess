import {
  Graph,
  Markup,
  NodeView,
  object_exports
} from "./chunk-5XJPUICH.js";
import {
  TemplateRef,
  ViewContainerRef
} from "./chunk-H4EWDFAJ.js";
import "./chunk-DP5J3HDO.js";
import "./chunk-KWSTWQNB.js";

// node_modules/@antv/x6-angular-shape/es/node.js
function getMarkup(primer) {
  const content = Markup.getForeignObjectMarkup();
  if (primer) {
    return [
      {
        tagName: primer,
        selector: "body"
      },
      content
    ];
  }
  return [content];
}
Graph.registerNode("angular-shape", {
  view: "angular-shape-view",
  markup: getMarkup(),
  attrs: {
    body: {
      fill: "none",
      stroke: "none",
      refWidth: "100%",
      refHeight: "100%"
    },
    fo: {
      refWidth: "100%",
      refHeight: "100%"
    }
  },
  propHooks(metadata) {
    if (metadata.markup == null) {
      const primer = metadata.primer;
      if (primer) {
        metadata.markup = getMarkup(primer);
        let attrs = {};
        switch (primer) {
          case "circle":
            attrs = {
              refCx: "50%",
              refCy: "50%",
              refR: "50%"
            };
            break;
          case "ellipse":
            attrs = {
              refCx: "50%",
              refCy: "50%",
              refRx: "50%",
              refRy: "50%"
            };
            break;
          default:
            break;
        }
        metadata.attrs = object_exports.merge({}, {
          body: Object.assign({ refWidth: null, refHeight: null }, attrs)
        }, metadata.attrs || {});
      }
    }
    return metadata;
  }
}, true);

// node_modules/@antv/x6-angular-shape/es/registry.js
var __rest = function(s, e) {
  var t = {};
  for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
    t[p] = s[p];
  if (s != null && typeof Object.getOwnPropertySymbols === "function")
    for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
      if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
        t[p[i]] = s[p[i]];
    }
  return t;
};
var registerInfo = /* @__PURE__ */ new Map();
function register(config) {
  const { shape, injector, content } = config, others = __rest(config, ["shape", "injector", "content"]);
  registerInfo.set(shape, { injector, content });
  Graph.registerNode(shape, Object.assign({ inherit: "angular-shape" }, others), true);
}

// node_modules/@antv/x6-angular-shape/es/view.js
var AngularShapeView = class _AngularShapeView extends NodeView {
  getNodeContainer() {
    return this.selectors && this.selectors.foContent;
  }
  confirmUpdate(flag) {
    const ret = super.confirmUpdate(flag);
    return this.handleAction(ret, _AngularShapeView.action, () => this.renderAngularContent());
  }
  getNgArguments() {
    var _a;
    const input = ((_a = this.cell.data) === null || _a === void 0 ? void 0 : _a.ngArguments) || {};
    return input;
  }
  /** 当执行 node.setData() 时需要对实例设置新的输入值 */
  setInstanceInput(content, ref) {
    const ngArguments = this.getNgArguments();
    if (content instanceof TemplateRef) {
      const embeddedViewRef = ref;
      embeddedViewRef.context = { ngArguments };
    } else {
      const componentRef = ref;
      Object.keys(ngArguments).forEach((v) => componentRef.setInput(v, ngArguments[v]));
      componentRef.changeDetectorRef.detectChanges();
    }
  }
  renderAngularContent() {
    const container = this.getNodeContainer();
    if (container) {
      this.unmountAngularContent();
      const node = this.cell;
      const { injector, content } = registerInfo.get(node.shape);
      const viewContainerRef = injector.get(ViewContainerRef);
      if (content instanceof TemplateRef) {
        const ngArguments = this.getNgArguments();
        const embeddedViewRef = viewContainerRef.createEmbeddedView(content, {
          ngArguments
        });
        embeddedViewRef.rootNodes.forEach((node2) => container.appendChild(node2));
        embeddedViewRef.detectChanges();
        node.on("change:data", () => this.setInstanceInput(content, embeddedViewRef));
      } else {
        const componentRef = viewContainerRef.createComponent(content, {
          injector
        });
        const insertNode = componentRef.hostView.rootNodes[0];
        container.appendChild(insertNode);
        this.setInstanceInput(content, componentRef);
        node.on("change:data", () => this.setInstanceInput(content, componentRef));
        node.on("removed", () => componentRef.destroy());
      }
    }
  }
  unmountAngularContent() {
    const container = this.getNodeContainer();
    container.innerHTML = "";
    return container;
  }
  onMouseDown(e, x, y) {
    const target = e.target;
    const tagName = target.tagName.toLowerCase();
    if (tagName === "input") {
      const type = target.getAttribute("type");
      if (type == null || [
        "text",
        "password",
        "number",
        "email",
        "search",
        "tel",
        "url"
      ].includes(type)) {
        return;
      }
    }
    super.onMouseDown(e, x, y);
  }
  unmount() {
    this.unmountAngularContent();
    super.unmount();
    return this;
  }
};
AngularShapeView.action = "angular";
AngularShapeView.config({
  bootstrap: [AngularShapeView.action],
  actions: {
    component: AngularShapeView.action
  }
});
NodeView.registry.register("angular-shape-view", AngularShapeView, true);
export {
  AngularShapeView,
  register,
  registerInfo
};
//# sourceMappingURL=@antv_x6-angular-shape.js.map
