<template>
  <div>
    <label class="block font-semibold text-heading mb-1">Category</label>
    <select
      v-model="model"
      required
      class="w-full border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading"
    >
      <option value="" disabled>Select category</option>
      <option v-for="cat in categories" :key="cat" :value="cat">{{ cat }}</option>
    </select>
  </div>
</template>

<script setup>
import { computed } from 'vue'
const props = defineProps({
  modelValue: String,
  categories: {
    type: Array,
    default: () => [],
  },
})
const emit = defineEmits(['update:modelValue'])

const model = computed({
  get: () => props.modelValue,
  set: (val) => emit('update:modelValue', val),
})
</script>
