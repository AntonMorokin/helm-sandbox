{{/*
    Template for chart fullname
*/}}
{{- define "chart.fullname" -}}
{{- .Chart.Name -}}-{{- .Chart.Version | replace "+" "_" -}}
{{- end -}}

{{/*
    Template for chart selector
*/}}
{{- define "chart.selector" -}}
helm.sh/chart: {{ include "chart.fullname" . }}
{{- end -}}

{{/*
    Template for generic app's labels
*/}}
{{- define "app.labels" -}}
app.kubernetes.io/instance: {{ .Release.Name }}
app.kubernetes.io/version: {{ .Chart.AppVersion }}
app.kubernetes.io/part-of: {{ .Values.global.systemName }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{ include "chart.selector" . }}
{{- end -}}