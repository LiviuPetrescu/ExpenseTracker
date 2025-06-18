<template>
  <div>
    <label class="block font-semibold text-heading mb-1">{{ label }}</label>
    <FlatPickr
      v-model="model"
      :placeholder="placeholder"
      class="w-full text-sm border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading"
    />
  </div>
</template>

<script setup>
import { computed } from 'vue'
import FlatPickr from 'vue-flatpickr-component'
import 'flatpickr/dist/flatpickr.css'

const props = defineProps({
  modelValue: String,
  label: String,
  placeholder: {
    type: String,
    default: 'Select Date',
  },
})

const emit = defineEmits(['update:modelValue'])

const model = computed({
  get: () => props.modelValue,
  set: (val) => emit('update:modelValue', val),
})
</script>
