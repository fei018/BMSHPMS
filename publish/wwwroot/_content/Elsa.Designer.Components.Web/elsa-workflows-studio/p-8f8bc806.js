import"./p-c74b54ba.js";import{a as t}from"./p-ccd51ea4.js";import"./p-971980b1.js";import"./p-82db2ff5.js";import"./p-949334ec.js";async function s(e,i){const s=i.options;let r;return r=s&&s.runtimeSelectListProviderType?await async function(e,i){const s=await t(e);return await s.designerApi.runtimeSelectItemsApi.get(i.runtimeSelectListProviderType,i.context||{})}(e,s):Array.isArray(s)?{items:s,isFlagsEnum:!1}:s,r||{items:[],isFlagsEnum:!1}}export{s as g};