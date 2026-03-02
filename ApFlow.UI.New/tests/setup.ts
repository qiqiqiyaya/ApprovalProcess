// Vitest setup for unit tests
// This file is run before all tests

// Suppress certain warnings during tests
const originalWarn = console.warn;
console.warn = (...args: unknown[]) => {
  const message = args[0];
  if (typeof message === 'string') {
    // Suppress zone.js warnings
    if (message.includes('Zone')) {
      return;
    }
    // Suppress import warnings
    if (message.includes('import')) {
      return;
    }
  }
  originalWarn.apply(console, args);
};
