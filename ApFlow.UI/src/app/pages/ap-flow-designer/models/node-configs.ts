/** 
 * 节点样式配置 
 */
export const NodeConfigs = {
  start: {
    shape: 'circle',
    width: 60,
    height: 60,
    attrs: {
      body: { fill: '#52c41a', stroke: '#52c41a' },
      label: { fill: '#fff', fontSize: 14 }
    }
  },
  end: {
    shape: 'circle',
    width: 60,
    height: 60,
    attrs: {
      body: { fill: '#ff4d4f', stroke: '#ff4d4f' },
      label: { fill: '#fff', fontSize: 14 }
    }
  },
  task: {
    shape: 'rect',
    width: 120,
    height: 40,
    attrs: {
      body: {
        stroke: '#d9d9d9',
        strokeWidth: 1,
        fill: '#fafafa',
        rx: 4,
        ry: 4,
      },
      label: { fontSize: 14 }
    }
  },
  parallel: {
    shape: 'rect',
    width: 100,
    height: 60,
    attrs: {
      body: {
        stroke: '#1890ff',
        strokeWidth: 2,
        fill: '#e6f7ff',
        rx: 4,
        ry: 4,
      },
      label: { fill: '#1890ff', fontSize: 14 }
    }
  },
  merge: {
    shape: 'rect',
    width: 100,
    height: 60,
    attrs: {
      body: {
        stroke: '#52c41a',
        strokeWidth: 2,
        fill: '#f6ffed',
        rx: 4,
        ry: 4,
      },
      label: { fill: '#52c41a', fontSize: 14 }
    }
  }
};