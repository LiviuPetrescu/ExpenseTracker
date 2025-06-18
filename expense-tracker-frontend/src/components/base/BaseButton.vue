<template>
  <button
    :type="buttonType"
    :disabled="disabled"
    :class="[
      'font-semibold px-4 py-2 rounded-xl transition',
      {
        'bg-blue-600 text-white hover:bg-blue-700': variant === 'primary',

        'bg-white text-blue-600 hover:bg-gray-100': variant === 'nav' && !active,
        'bg-gray-200 text-blue-700 border-2 border-blue-700 font-semibold':
          variant === 'nav' && active,

        'opacity-50 cursor-not-allowed': disabled,
      },
    ]"
  >
    <slot />
  </button>
</template>

<script setup>
defineProps({
  variant: {
    type: String,
    default: 'primary',
    validator: (val) => ['primary', 'nav'].includes(val),
  },
  active: {
    type: Boolean,
    default: false,
  },
  buttonType: {
    type: String,
    default: 'button',
  },
  disabled: {
    type: Boolean,
    default: false,
  },
  customClass: {
    type: String,
    default: '',
  },
})
</script>
