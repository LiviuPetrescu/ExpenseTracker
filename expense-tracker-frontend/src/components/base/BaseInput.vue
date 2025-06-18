<template>
  <div>
    <label v-if="label" class="block font-semibold text-heading mb-1">{{ label }}</label>

    <component
      :is="multiline ? 'textarea' : 'input'"
      :value="model"
      @input="onInput"
      :type="multiline ? undefined : type"
      :rows="multiline ? rows : undefined"
      :required="required"
      class="w-full border border-border rounded px-2 py-1.5 focus:outline-none focus:ring-2 focus:ring-heading resize-none"
    />
  </div>
</template>

<script setup>
import { computed } from 'vue'

const props = defineProps({
  modelValue: [String, Number],
  label: String,
  type: {
    type: String,
    default: 'text',
  },
  required: {
    type: Boolean,
    default: false,
  },
  multiline: {
    type: Boolean,
    default: false,
  },
  rows: {
    type: Number,
    default: 2,
  },
})

const emit = defineEmits(['update:modelValue'])

const model = computed({
  get: () => props.modelValue,
  set: (val) => emit('update:modelValue', val),
})

const onInput = (event) => {
  emit('update:modelValue', event.target.value)
}
</script>
